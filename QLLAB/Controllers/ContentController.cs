using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLLAB.Models;
using QLLAB.Services.Interfaces;

namespace QLLAB.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        private readonly IBlobStorageImageService _blobStorageImageService;

        public ContentController(IBlobStorageImageService blobStorageImageService)
        {
            _blobStorageImageService = blobStorageImageService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody]Content content)
        {
            try
            {
                var downloadImage = await _blobStorageImageService.SaveAsync(content);
                return Ok(downloadImage);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
