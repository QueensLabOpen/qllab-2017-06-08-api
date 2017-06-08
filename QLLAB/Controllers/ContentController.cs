using System;
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
                    Url = string.Format(BasePath, Guid.NewGuid())
                });

                return Ok(downloadImage);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
