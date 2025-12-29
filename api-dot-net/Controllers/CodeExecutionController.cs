
using System.Security.Claims;
using System.Threading.Tasks;
using CjsApi.Dto.RequestDto;
using CjsApi.Dto.ResponseDto;
using CjsApi.Services;
using CjsApi.Services.CodeExecution.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CjsApi.Controllers
{

    [ApiController]
    [Route("api/code")]
    public class CodeExecutionController : ControllerBase
    {

        private readonly CodeExecutionService _service;
        private readonly SubmissionService _submissionService;

        public CodeExecutionController(CodeExecutionService service, SubmissionService submissionService)
        {
            _service = service;
            _submissionService = submissionService;
        }

        [Authorize]
        [HttpPost("run")]
        public async Task<ActionResult<ApiResponseDto<string>>> RunCode(
        [FromBody] CodeRunRequestDto dto)
        {
            try
            {
                var jobId = await _service.RunAsync(dto);


                return Ok(new ApiResponseDto<string>(
                    true,
                    "Execution started",
                    jobId
                ));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponseDto<string>(
                                    false,
                                    e.Message,
                                    null
                                ));

            }
        }


        [Authorize]
        [HttpPost("submit")]
        public async Task<ActionResult<ApiResponseDto<string>>> SubmitCode(
       [FromBody] CodeRunRequestDto dto)
        {
            try
            {


                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!int.TryParse(userIdClaim, out int userId))
                    return Unauthorized("Invalid user id in token");


                var s = await _submissionService.CreateSubmissionAsync(userId, dto.ProblemId, dto.SourceCode, dto.Language);
                var jobId = await _service.SubmitAsync(dto, s.Id);

                return Ok(new ApiResponseDto<string>(
                    true,
                    "Execution started",
                    jobId
                ));

            }
            catch (Exception e)
            {


                return Ok(new ApiResponseDto<string>(
                    true,
                    "Execution started",
                    e.Message
                ));
            }
        }

        // 2️⃣ Poll status
        [HttpGet("status/{jobId}")]
        public ActionResult<ApiResponseDto<object>> GetStatus(string jobId)
        {
            var job = _service.GetStatus(jobId);

            if (job == null)
                return NotFound();
            return Ok(new ApiResponseDto<ExecutionJob>(
                true,
                "Status fetched",
                job
            ));
        }

    }
}