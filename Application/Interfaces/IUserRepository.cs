using Domain;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetByEmail(string email);
    Task<User> AddAsync(User user);
}