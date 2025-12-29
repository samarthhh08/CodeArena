using CjsApi.Dto.RequestDto;
using CjsApi.Dto.ResponseDto;
using CjsApi.Models;
using Microsoft.AspNetCore.Identity;
using CjsApi.Services.UserService;



public sealed class AuthService
{
    private readonly UserService _userService;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public AuthService(UserService userService)
    {
        _userService = userService;
    }

    public async Task<AuthUserDto> SignInAsync(
        SignInRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userService.FindUserByEmailAsync(
            request.Email,
            cancellationToken)
            ?? throw new Exception("Invalid email or password.");

        var verificationResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new Exception("Invalid email or password.");
        }

        // 🔐 Auto-rehash if needed
        if (verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            // save updated hash if you expose update method
        }

        return new AuthUserDto(
            user.Id.ToString(),
            user.Email,
            user.Username,
            user.Role
        );
    }

    public async Task<AuthUserDto> SignUpAsync(
        SignUpRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var existingUser = await _userService.FindUserByEmailAsync(
            request.Email,
            cancellationToken);

        if (existingUser != null)
            throw new Exception("Email already in use.");

        var newUser = await _userService.CreateUserAsync(
            request,
            cancellationToken);

        return new AuthUserDto(
            newUser.Id.ToString(),
            newUser.Email,
            newUser.Username,
            newUser.Role
        );
    }

    public async Task<AuthUserDto> GetUserInfoAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var user = await _userService.FindUserByIdAsync(id, cancellationToken)
            ?? throw new Exception("Invalid user id.");

        return new AuthUserDto(
            user.Id.ToString(),
            user.Email,
            user.Username,
            user.Role
        );
    }
}
