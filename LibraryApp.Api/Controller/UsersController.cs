using System.Threading.Tasks;
using DinnerApp.Infrastructure.Persistence;
using LibraryApp.Api.Models;
using LibraryApp.Domain.Users;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controller;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
      var users = await _context.Users
          .OrderBy(u => u.CreatedAt)
          .Take(10)
          .Select(u => u.Adapt<UserResponse>()).ToListAsync();
      return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId.Create(id));
      if (user is null)
      {
        return NotFound(new { Message = "User not found" });
      }
      
      return Ok(user.Adapt<UserDetailResponse>());
    }
}