namespace Infrastructure;

public interface IPasswordHasher
{
    string HashPassword(string password);
}