using Microsoft.AspNetCore.Mvc;
using QLLAB.Models;

namespace QLLAB.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            Answer a = new Answer("http://via.placeholder.com/1080x480", false);
            Answer b = new Answer("http://via.placeholder.com/1080x480", false);
            Answer c = new Answer("http://via.placeholder.com/1080x480?text=Winner", true);
            Answer d = new Answer("http://via.placeholder.com/1080x480", false);

            Question question = new Question("tag4");
            question.Answers.Add(a);
            question.Answers.Add(b);
            question.Answers.Add(c);
            question.Answers.Add(d);
            return Ok(question);
        }

    }
}
