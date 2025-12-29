
using System.Security.Cryptography.X509Certificates;
using CjsApi.Dto;
using CjsApi.Dto.RequestDto;
using CjsApi.Models;
using CjsApi.Repositories.SubmissionRepository;
using CjsApi.Repositories.UserRepository;
using Microsoft.AspNetCore.Identity;

namespace CjsApi.Services.UserService;

public sealed class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly ISubmissionRepository _submissionRepository;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public UserService(IUserRepository userRepository, ISubmissionRepository submissionRepository)
    {
        _userRepository = userRepository;
        _submissionRepository = submissionRepository;
    }

    public async Task<User> CreateUserAsync(
        SignUpRequestDto request,
        CancellationToken cancellationToken = default)
    {
        // 1️⃣ Check if user already exists
        if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            throw new InvalidOperationException("User already exists");
        }

        // 2️⃣ Create user entity
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Role = Role.USER
        };

        // 3️⃣ Hash password (CRITICAL)
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        // 4️⃣ Save user
        return await _userRepository.AddAsync(user, cancellationToken);
    }

    public async Task<User?> FindUserByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _userRepository.GetByEmailAsync(email, cancellationToken);
    }

    public async Task<User?> FindUserByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _userRepository.GetByIdAsync(id, cancellationToken);
    }


    public async Task<UserProfileDto> GetUserProfile(int userId)
    {

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("Invalid user id");
        }

        var userSubmissions = await _submissionRepository.GetByUserAsync(userId);

        var profile = new UserProfileDto
        {
            Username = user.Username,
            Email = user.Email,
            About = "",
            LatestSubmissions = userSubmissions
                .Select(s => new ProblemSubmissionDetails
                {
                    Title = s.Problem.Title,      // assuming navigation property
                    Status = s.Status
                })
                .ToList()
        };

        return profile;
    }

   


}
