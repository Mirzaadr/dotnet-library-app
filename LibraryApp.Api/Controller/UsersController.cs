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
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet] // admin
    public async Task<IActionResult> GetAllUsers()
    {
        // TODO: implement function to get all user general data
        var users = await _context.Users
          .OrderBy(u => u.CreatedAt)
          .Take(10)
          .Select(u => u.Adapt<UserResponse>()).ToListAsync();
      return Ok(users);
    }

    [HttpGet("{id}")] // admin
    public async Task<IActionResult> GetUserById(Guid id)
    {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId.Create(id));
      if (user is null)
      {
        return NotFound(new { Message = "User not found" });
      }
      
      return Ok(user.Adapt<UserDetailResponse>());
    }

    [HttpPut("{id}/status")] // admin
    public IActionResult UpdateUserStatus(Guid id, [FromBody] bool isActive)
    {
        //TODO: implement update user status (active / inactive)
        return Ok(id);
    }

    [HttpPut("{id}/role")] // admin
    public IActionResult UpdateUserRole(Guid id)
    {
        //TODO: implement update user role      
        return Ok(id);
    }
}