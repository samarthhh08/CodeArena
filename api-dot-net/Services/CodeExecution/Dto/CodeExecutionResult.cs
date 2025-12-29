using CjsApi.Dto.ResponseDto;
using CjsApi.Models;

namespace CjsApi.Services.CodeExecution.Dto
{

    public class CodeExecutionResult
    {
        public string Output { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public int ExitCode { get; set; }
        public bool TimedOut { get; set; }

        public SubmissionStatus SubmissionStatus {get;set;} = SubmissionStatus.PENDING;

        public List<TestCaseResultDto> TestCaseResults {get;set;} = new();
    }
}