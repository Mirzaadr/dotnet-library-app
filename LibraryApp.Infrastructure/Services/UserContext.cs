using System.Security.Claims;
using LibraryApp.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace LibraryApp.Infrastructure.Services;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId => Guid.Parse(
        _httpContextAccessor
            .HttpContext?
            .User
            .FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new ApplicationException("User context is unavailable")
    );
}