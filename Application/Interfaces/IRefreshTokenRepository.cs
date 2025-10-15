using Domain;

namespace Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetByUserId(Guid userId);
    Task UpdateAsync(RefreshToken token);
}