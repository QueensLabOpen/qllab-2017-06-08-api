using Microsoft.AspNetCore.Mvc;

namespace QLLAB.Controllers
{
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            string[] tags = { "Tag1", "Tag2", "Tag3", "Tag4" };

            return Ok(tags);

        }

    }
}
