
using System.Threading.Tasks;
using CjsApi.Dto.RequestDto;
using CjsApi.Dto.ResponseDto;
using CjsApi.Services;
using CjsApi.Services.CodeExecution.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CjsApi.Controllers
{

    [ApiController]
    [Route("api/code")]
    public class CodeExecutionController : ControllerBase
    {

        private readonly CodeExecutionService _service;

        public CodeExecutionController(CodeExecutionService service)
        {
            _service = service;
        }

        [HttpPost("run")]
        public async Task<ActionResult<ApiResponseDto<string>>> RunCode(
        [FromBody] CodeRunRequestDto dto)
        {
            var jobId = await _service.RunAsync(dto);

            return Ok(new ApiResponseDto<string>(
                true,
                "Execution started",
                jobId
            ));
        }


        [HttpPost("submit")]
        public async Task<ActionResult<ApiResponseDto<string>>> SubmitCode(
        [FromBody] CodeRunRequestDto dto)
        {
            var jobId = await _service.SubmitAsync(dto);

            return Ok(new ApiResponseDto<string>(
                true,
                "Execution started",
                jobId
            ));
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