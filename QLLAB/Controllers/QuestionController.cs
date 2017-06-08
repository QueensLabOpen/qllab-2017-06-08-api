using Microsoft.AspNetCore.Mvc;
using QLLAB.Models;
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


            Question question = _questionService.GetRandom();
            return Ok(question);
        }

    }
}
