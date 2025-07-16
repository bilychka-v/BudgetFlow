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

    public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
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

    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);
        
        var result = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);
        
        if(result == false)
            throw new Exception("Invalid password");
        
        var token = _jwtProvider.GenerateToken(user);
        
        return token;
    }
    
}