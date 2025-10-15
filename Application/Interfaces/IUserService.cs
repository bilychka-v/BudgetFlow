using BudgetFlow.Dtos;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserRegisterDto> Register(string username, string email, string password);
    Task<LoginResponseDto> Login(string email, string password);
}