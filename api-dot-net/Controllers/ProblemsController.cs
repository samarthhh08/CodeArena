
using CjsApi.Dto;
using CjsApi.Dto.RequestDto;
using CjsApi.Dto.ResponseDto;
using CjsApi.Models;
using CjsApi.Services;
using CjsApi.Services.CodeExecution.Dto;
using CjsApi.Services.ProblemService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CjsApi.Controllers
{

    [ApiController]
    [Route("api/problems")]
    public class ProblemsController : ControllerBase
    {

        private IProblemService _problemService;

        public ProblemsController(IProblemService problemService)
        {
            _problemService = problemService;

        }
        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<GetAllProblemDto>>> GetAllProblems(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
            )
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0 || pageSize > 50) pageSize = 10;

            // 1️⃣ Fetch all (or IQueryable ideally)
            var problemsQuery = _problemService.GetProblemsQueryable(
                difficulty: null,
                tags: null
            );

            // 2️⃣ Total count (before pagination)
            var total = await problemsQuery.CountAsync();

            // 3️⃣ Apply pagination
            var problems = await problemsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responseDto = new GetAllProblemDto
            {
                Problems = problems.Select(p => new ProblemMetaDataDto
                {
                    Title = p.Title,
                    Difficulty = p.Difficulty,
                    Slug = p.Slug
                }).ToList(),

                Total = total,
                Page = page,
                PageSize = pageSize
            };

            return Ok(new ApiResponseDto<GetAllProblemDto>(
                true,
                "Problems fetched successfully",
                responseDto
            ));
        }


        [HttpGet("{slug}")]
        public async Task<ActionResult<ApiResponseDto<GetProblemDto>>> GetProblem(string slug)
        {
            var problem = await _problemService.GetProblemBySlugAsync(slug);

            if (problem == null)
            {
                return NotFound(new ApiResponseDto<GetProblemDto>(
                    false,
                    "Problem not found",
                    null
                ));
            }

            var dto = new GetProblemDto
            {
                Id = problem.Id,
                Title = problem.Title,
                Description = problem.Description,
                Difficulty = problem.Difficulty,

                Tags = problem.ProblemTags
                    .Select(pt => pt.Tag.Name)
                    .ToList(),

                SampleTestCases = problem.TestCases
                    .Where(tc => tc.IsSample)
                    .Select(tc => new SampleTestCaseDto
                    {
                        Input = tc.Input,
                        Output = tc.ExpectedOutput
                    })
                    .ToList()
            };

            return Ok(new ApiResponseDto<GetProblemDto>(
                true,
                "Problem fetched successfully",
                dto
            ));
        }




    }


}