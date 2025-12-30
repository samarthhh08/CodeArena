using CjsApi.Dto;
using CjsApi.Dto.ResponseDto;
using CjsApi.Services;
using CjsApi.Services.ProblemService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;


namespace CjsApi.Controllers
{



    [ApiController]
    [Route("api/ai")]
    public class AiController : ControllerBase
    {
        private readonly GeminiService _gemini;
        private readonly IProblemService _problemService;

        public AiController(GeminiService gemini, IProblemService problemService)
        {
            _gemini = gemini;
            _problemService = problemService;
        }

        [EnableRateLimiting("ai")]
        [HttpPost("code-review")]
        public async Task<IActionResult> CodeReview([FromBody] AiRequestDto dto)
        {

            var problem = await _problemService.GetProblemByIdAsync(dto.ProblemId);

            if (problem == null)
            {
                return BadRequest(new ApiResponseDto<string>(
                    false,
                    "invalid problem id",
                    null
                ));
            }
            var prompt = PromptBuilder.CodeReviewPrompt(
                problem.Description,
                dto.Code,
                dto.Language
            // dto.Result
            );

            var response = await _gemini.GenerateAsync(prompt);
            return Ok(new { feedback = response });
        }
    }

}
