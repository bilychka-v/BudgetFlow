using Domain;

namespace Infrastructure;

public interface IJwtProvider
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
}