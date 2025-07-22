using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Domain.Users;

public class RefreshToken : Entity<int>
{
    public string Token { get; private set; } = string.Empty;
    public UserId UserId { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; } = false;
    public DateTime CreatedAt { get; private set; }

    private RefreshToken() {}

    public RefreshToken(string token, UserId userId, DateTime expiresAt)
    {
        Token = token;
        UserId = userId;
        ExpiresAt = expiresAt;
        CreatedAt = DateTime.Now;
    }

    public void Update(string token, DateTime expiresAt)
    {
        Token = token;
        ExpiresAt = expiresAt;
    }

    public void Revoke() => IsRevoked = true;

    public bool IsActive => !IsRevoked && ExpiresAt > DateTime.Now;
}
