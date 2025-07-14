using Application.Interfaces;
using Db;
using Domain;

namespace Application.Repositories;

public class UserRepository:IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user;
    }
}