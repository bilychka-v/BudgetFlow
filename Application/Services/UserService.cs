using Application.Interfaces;
using BudgetFlow.Dtos;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider, IRefreshTokenRepository refreshTokenRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<UserRegisterDto> Register(string username, string email,  string password)
    {
        var passwordHash = _passwordHasher.HashPassword(password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = username,
            Email = email,
            PasswordHash = passwordHash
        };
        
        await _userRepository.AddAsync(user);
        
        var userRegisterDto = new UserRegisterDto
            (
                user.UserName, 
                user.Email, 
                user.PasswordHash
            );
        
        return userRegisterDto;
        
    }

    public async Task<LoginResponseDto> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);
        if (user == null)
            throw new Exception("User not found");

        var result = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);
        if (!result)
            throw new Exception("Invalid password");

        var token = _jwtProvider.GenerateToken(user);
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = _jwtProvider.GenerateRefreshToken(),
            UserId = user.Id,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        await _refreshTokenRepository.AddAsync(refreshToken);

        return new LoginResponseDto(token, user.UserName, refreshToken.Token);
    }

    
}