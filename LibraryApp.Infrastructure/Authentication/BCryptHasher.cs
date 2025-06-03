// using BCrypt;
using BCrypt.Net;
using LibraryApp.Application.Abstractions.Authentication;

namespace LibraryApp.Infrastructure.Authentication;

internal sealed class BCryptHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        string hash = BCrypt.Net.BCrypt.HashPassword(password, 10);
        return hash;
    }

    public bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}