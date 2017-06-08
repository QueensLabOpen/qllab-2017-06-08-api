using Microsoft.AspNetCore.Mvc;
using QLLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLLAB.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            string[] tags = { "tag1", "tag2", "tag3" };
            Answer a = new Answer("theUrl", tags);
            Answer b = new Answer("theUrl", tags);
            string[] winnerTags = { "tag1", "tag2", "tag3", "tag4"};
            Answer c = new Answer("theUrl", winnerTags);

            Question question = new Question("tag4");
            question.answers.Add(a);
            question.answers.Add(b);
            question.answers.Add(c);

            return Ok(question);
        }

    }
}
