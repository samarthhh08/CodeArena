
using System.Threading.Tasks;
using CjsApi.Dto.RequestDto;
using CjsApi.Services.CodeExecution.Base;
using CjsApi.Services.CodeExecution.Dto;
using CjsApi.Services.CodeExecution.Factory;
using CjsApi.Services.CodeExecution.Store;
using CjsApi.Services.CodeExecution.Worker;
using CjsApi.Services.ProblemService;

namespace CjsApi.Services
{

    public class CodeExecutionService
    {
        private readonly ExecutionJobStore _jobStore;
        private readonly CodeExecutionWorker _worker;

        private readonly IProblemService _problemService;


        public CodeExecutionService(
            ExecutionJobStore jobStore,
            CodeExecutionWorker worker, IProblemService problemService)
        {
            _jobStore = jobStore;
            _worker = worker;
            _problemService = problemService;
        }

        public async Task<string> RunAsync(CodeRunRequestDto dto)
        {
            var job = _jobStore.CreateJob();

            Console.WriteLine($"📥 Submit called. JobId = {job.JobId}");

            // 1️⃣ Fetch problem
            var problem = await _problemService.GetProblemByIdAsync(dto.ProblemId);

            if (problem == null)
                throw new Exception("Problem not found");

            // 2️⃣ Prepare SAMPLE test cases only
            var testCases = problem.TestCases
                            .Where(tc => tc.IsSample)
                            .Select(tc => new TestCaseDto(
                                tc.Input,
                                tc.ExpectedOutput
                            ))
                            .ToList();

            // 3️⃣ Build execution request
            var request = new CodeExecutionRequest
            {
                Language = dto.Language,
                SourceCode = dto.SourceCode,
                TestCases = testCases
            };

            // 4️⃣ Enqueue job
            _worker.Enqueue(job, request);

            return job.JobId;
        }




        public async Task<string> SubmitAsync(CodeRunRequestDto dto ,int submissionId)
        {
            var job = _jobStore.CreateJob(submissionId);

            Console.WriteLine($"📥 Submit called. JobId = {job.JobId}");

            // 1️⃣ Fetch problem
            var problem = await _problemService.GetProblemByIdAsync(dto.ProblemId);

            if (problem == null)
                throw new Exception("Problem not found");

            // 2️⃣ Prepare SAMPLE test cases only
            var testCases = problem.TestCases
                            .Select(tc => new TestCaseDto(
                                tc.Input,
                                tc.ExpectedOutput
                            ))
                            .ToList();



            // 3️⃣ Build execution request
            var request = new CodeExecutionRequest
            {
                Language = dto.Language,
                SourceCode = dto.SourceCode,
                TestCases = testCases
            };

            // 4️⃣ Enqueue job
            _worker.Enqueue(job, request);

            return job.JobId;
        }


        public ExecutionJob? GetStatus(string jobId)
            => _jobStore.GetJob(jobId);
    }
}