using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QLLAB.Models;
using QLLAB.Repositories.Interfaces;
using QLLAB.Services.Interfaces;

namespace QLLAB.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        private readonly IBlobStorageImageService _blobStorageImageService;
        private readonly IImageRepository _imageRepository;
        private readonly IOptions<AppSettings> _appSettings;

        public ContentController(IBlobStorageImageService blobStorageImageService, IImageRepository imageRepository, IOptions<AppSettings> appSettings)
        {
            _blobStorageImageService = blobStorageImageService;
            _imageRepository = imageRepository;
            _appSettings = appSettings;
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
                    Url = string.Format(_appSettings.Value.ImageBasePath, downloadImage.BlobId)
                });

                return Ok(downloadImage);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var blobImage = await _blobStorageImageService.GetAsync(id);
                if (blobImage == null)
                    return NotFound();

                var mimeType = GetMimeType(Path.GetExtension(blobImage.Filename));
                HttpContext.Response.ContentType = mimeType;
                return File(blobImage.ByteData, mimeType);
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
