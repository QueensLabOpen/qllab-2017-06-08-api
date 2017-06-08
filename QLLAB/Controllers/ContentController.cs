using Microsoft.AspNetCore.Mvc;

namespace QLLAB.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return Ok("Tjena");
        }
    }
}
