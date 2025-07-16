using BudgetFlow.Dtos;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserRegisterDto> Register(string username, string email, string password);
    Task<string> Login(string email, string password);
}