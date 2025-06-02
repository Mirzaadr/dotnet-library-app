using DinnerApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controller;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthenticationController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public IActionResult Register()
    {
      return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
      return Ok();
    }
}