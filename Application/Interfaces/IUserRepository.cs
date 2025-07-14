using Domain;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
}