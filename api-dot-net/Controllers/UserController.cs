using System.Security.Claims;
using CjsApi.Dto;
using CjsApi.Dto.ResponseDto;
using CjsApi.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CjsApi.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {


        private readonly UserService _userService;
        private readonly SubmissionService _submissionService;

        public UserController(UserService userService, SubmissionService submissionService)
        {
            _userService = userService;
            _submissionService = submissionService;
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<ApiResponseDto<UserProfileDto>>> GetUserProfile()
        {

            try
            {
               
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
               

                if (!int.TryParse(userIdClaim, out int userId))
                    return Unauthorized("Invalid user id in token");

                UserProfileDto profile = await _userService.GetUserProfile(userId);
                Console.WriteLine(profile.Email);

                return Ok(new ApiResponseDto<UserProfileDto>(
                    true,
                    "User information retrieved successfully",
                    profile
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
        [HttpGet("submissions/{problemId:int}")]
        public async Task<ActionResult<ApiResponseDto<List<ProblemSubmissionDetails>>>>
    GetProblemSubmissions(int problemId)
        {
            try
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!int.TryParse(userIdClaim, out int userId))
                    return Unauthorized("Invalid user id in token");

                var submissions = await _submissionService
                    .GetProblemSubmissionsAsync(userId, problemId);

                return Ok(new ApiResponseDto<List<ProblemSubmissionDetails>>(
                    true,
                    "Problem submissions retrieved successfully",
                    submissions
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

    }





}