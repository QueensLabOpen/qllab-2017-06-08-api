using Microsoft.AspNetCore.Mvc;

namespace QLLAB.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        [Route("")]
        public IActionResult Get()
        {
            return Ok("Tjena");
        }
    }
}
