using Microsoft.AspNetCore.Mvc;
using QLLAB.Services.Interfaces;

namespace QLLAB.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var question = _questionService.GetRandom();
            return Ok(question);
        }

    }
}
