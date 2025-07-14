using Application.Interfaces;
using BudgetFlow.Dtos;
using Domain;
using Infrastructure;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
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
                user.Id, 
                user.UserName, 
                user.Email, 
                user.PasswordHash
            );
        
        return userRegisterDto;
        
    }
    
}