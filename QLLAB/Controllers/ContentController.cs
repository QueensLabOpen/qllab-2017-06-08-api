using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLLAB.Models;
using QLLAB.Repositories.Interfaces;
using QLLAB.Services.Interfaces;

namespace QLLAB.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        private const string BasePath = "http://qllab1-api.azurewebsites.net/api/content/{0}";
        private readonly IBlobStorageImageService _blobStorageImageService;
        private readonly IImageRepository _imageRepository;

        public ContentController(IBlobStorageImageService blobStorageImageService, IImageRepository imageRepository)
        {
            _blobStorageImageService = blobStorageImageService;
            _imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody]Content content)
        {
            try
            {
                var downloadImage = await _blobStorageImageService.SaveAsync(content);
                await _imageRepository.SaveImageAsync(new Image
                {
                    Filename = content.Filename,
                    Tags = content.Tags,
                    Url = string.Format(BasePath, downloadImage.BlobId)
                });

                return Ok(downloadImage);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var blobImage = await _blobStorageImageService.GetAsync(id);
                if (blobImage == null)
                    return NotFound();

                var mimeType = GetMimeType(Path.GetExtension(blobImage.Filename));
                return File(blobImage.Data, mimeType);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        private static string GetMimeType(string extension)
        {
            var allowed = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {".ai", "application/postscript"},
                {".bmp", "image/bmp"},
                {".eps", "application/postscript"},
                {".gif", "image/gif"},
                {".ico", "image/x-icon"},
                {".jpe", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".jpg", "image/jpeg"},
                {".mac", "image/x-macpaint"},
                {".png", "image/png"},
            };
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            if (!extension.StartsWith("."))
                extension = "." + extension;

            string mime;
            return allowed.TryGetValue(extension, out mime) ? mime : "image/jpeg";

        }
    }
}
