using CjsApi.Dto.RequestDto;
using CjsApi.Dto.ResponseDto;
using CjsApi.Services.McqService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CjsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class McqController : ControllerBase
    {
        private readonly IMcqService _mcqService;

        public McqController(IMcqService mcqService)
        {
            _mcqService = mcqService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ApiResponseDto<McqQuestionWithAnswerDto>>> CreateQuestion([FromBody] CreateMcqDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var question = await _mcqService.CreateQuestionAsync(dto, userId);
                
                return Ok(new ApiResponseDto<McqQuestionWithAnswerDto>(
                    true,
                    "MCQ question created successfully",
                    question
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<McqQuestionWithAnswerDto>(
                    false,
                    ex.Message
                ));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponseDto<List<McqQuestionWithAnswerDto>>>> GetAllQuestions([FromQuery] string? category = null)
        {
            try
            {
                var questions = await _mcqService.GetAllQuestionsAsync(category);
                
                return Ok(new ApiResponseDto<List<McqQuestionWithAnswerDto>>(
                    true,
                    "Questions retrieved successfully",
                    questions
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<List<McqQuestionWithAnswerDto>>(
                    false,
                    ex.Message
                ));
            }
        }

        [HttpGet("categories")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponseDto<List<string>>>> GetCategories()
        {
            try
            {
                var categories = await _mcqService.GetCategoriesAsync();
                
                return Ok(new ApiResponseDto<List<string>>(
                    true,
                    "Categories retrieved successfully",
                    categories
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<List<string>>(
                    false,
                    ex.Message
                ));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ApiResponseDto<McqQuestionWithAnswerDto>>> GetQuestion(int id)
        {
            try
            {
                var question = await _mcqService.GetQuestionByIdAsync(id);
                if (question == null)
                {
                    return NotFound(new ApiResponseDto<McqQuestionWithAnswerDto>(
                        false,
                        "Question not found"
                    ));
                }
                
                return Ok(new ApiResponseDto<McqQuestionWithAnswerDto>(
                    true,
                    "Question retrieved successfully",
                    question
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<McqQuestionWithAnswerDto>(
                    false,
                    ex.Message
                ));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ApiResponseDto<McqQuestionWithAnswerDto>>> UpdateQuestion(int id, [FromBody] CreateMcqDto dto)
        {
            try
            {
                var question = await _mcqService.UpdateQuestionAsync(id, dto);
                
                return Ok(new ApiResponseDto<McqQuestionWithAnswerDto>(
                    true,
                    "Question updated successfully",
                    question
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<McqQuestionWithAnswerDto>(
                    false,
                    ex.Message
                ));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteQuestion(int id)
        {
            try
            {
                var result = await _mcqService.DeleteQuestionAsync(id);
                
                return Ok(new ApiResponseDto<bool>(
                    result,
                    result ? "Question deleted successfully" : "Question not found",
                    result
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<bool>(
                    false,
                    ex.Message
                ));
            }
        }

        [HttpPost("quiz/start")]
        [Authorize]
        public async Task<ActionResult<ApiResponseDto<QuizSessionDto>>> StartQuiz([FromBody] StartQuizDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var session = await _mcqService.StartQuizAsync(dto, userId);
                
                return Ok(new ApiResponseDto<QuizSessionDto>(
                    true,
                    "Quiz started successfully",
                    session
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<QuizSessionDto>(
                    false,
                    ex.Message
                ));
            }
        }

        [HttpGet("quiz/{sessionId}/questions")]
        [Authorize]
        public async Task<ActionResult<ApiResponseDto<List<McqQuestionDto>>>> GetQuizQuestions(int sessionId)
        {
            try
            {
                var questions = await _mcqService.GetQuizQuestionsAsync(sessionId);
                
                return Ok(new ApiResponseDto<List<McqQuestionDto>>(
                    true,
                    "Quiz questions retrieved successfully",
                    questions
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<List<McqQuestionDto>>(
                    false,
                    ex.Message
                ));
            }
        }

        [HttpPost("quiz/submit")]
        [Authorize]
        public async Task<ActionResult<ApiResponseDto<McqAttemptResultDto>>> SubmitAnswer([FromBody] SubmitMcqAnswerDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var result = await _mcqService.SubmitAnswerAsync(dto, userId);
                
                return Ok(new ApiResponseDto<McqAttemptResultDto>(
                    true,
                    "Answer submitted successfully",
                    result
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<McqAttemptResultDto>(
                    false,
                    ex.Message
                ));
            }
        }

        [HttpGet("quiz/{sessionId}/result")]
        [Authorize]
        public async Task<ActionResult<ApiResponseDto<QuizResultDto>>> GetQuizResult(int sessionId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var result = await _mcqService.GetQuizResultAsync(sessionId, userId);
                
                return Ok(new ApiResponseDto<QuizResultDto>(
                    true,
                    "Quiz result retrieved successfully",
                    result
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<QuizResultDto>(
                    false,
                    ex.Message
                ));
            }
        }

        [HttpGet("quiz/history")]
        [Authorize]
        public async Task<ActionResult<ApiResponseDto<List<QuizSessionDto>>>> GetQuizHistory()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var history = await _mcqService.GetUserQuizHistoryAsync(userId);
                
                return Ok(new ApiResponseDto<List<QuizSessionDto>>(
                    true,
                    "Quiz history retrieved successfully",
                    history
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<List<QuizSessionDto>>(
                    false,
                    ex.Message
                ));
            }
        }

        [HttpPost("attempt")]
        [Authorize]
        public async Task<ActionResult<ApiResponseDto<bool>>> SaveAttempt([FromBody] SaveMcqAttemptDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                await _mcqService.SaveAttemptAsync(dto, userId);
                
                return Ok(new ApiResponseDto<bool>(
                    true,
                    "Attempt saved successfully",
                    true
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<bool>(
                    false,
                    ex.Message,
                    false
                ));
            }
        }
    }
}