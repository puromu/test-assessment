using Assessment.Application.DTOs;
using Assessment.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Api.Controllers
{
    [ApiController]
    [Route("api/assessment")]
    public class AssessmentController : ControllerBase
    {
        private readonly AssessmentService _assessmentService;

        public AssessmentController(AssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _assessmentService.GetQuestionsAsync();
            return Ok(questions);
        }

        [HttpPost("results")]
        public async Task<IActionResult> SaveResult([FromBody] SubmitResultRequest request)
        {
            var result = await _assessmentService.SaveResultAsync(request);
            return Ok(result);
        }

        [HttpGet("results")]
        public async Task<IActionResult> GetResults()
        {
            var results = await _assessmentService.GetResultsAsync();
            return Ok(results);
        }
    }
}
