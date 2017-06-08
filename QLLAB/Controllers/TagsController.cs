using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLLAB.Controllers
{
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        [Route("")]
        public IActionResult Get()
        {
            string[] tags = { "Tag1", "Tag2", "Tag3", "Tag4" };

            return Ok(tags);

        }

    }
}
