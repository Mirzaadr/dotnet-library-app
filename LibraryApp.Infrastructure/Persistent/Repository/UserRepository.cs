using DinnerApp.Infrastructure.Persistence;
using LibraryApp.Application.Abstractions.Data;
using LibraryApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Persistent.Repository;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _context;

  public UserRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    return await _context.Users.FindAsync(new object[] { id }, cancellationToken);
  }

  public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
  {
    return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
  }

  public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    return await _context.Users.ToListAsync(cancellationToken);
  }

  public async Task AddAsync(User user, CancellationToken cancellationToken = default)
  {
    await _context.Users.AddAsync(user, cancellationToken);
    await _context.SaveChangesAsync(cancellationToken);
  }

  public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
  {
    _context.Users.Update(user);
    await _context.SaveChangesAsync(cancellationToken);
  }

  public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    var user = await GetByIdAsync(id, cancellationToken);
    if (user != null)
    {
      _context.Users.Remove(user);
      await _context.SaveChangesAsync(cancellationToken);
    }
  }

  Task<User?> IUserRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  Task<User?> IUserRepository.GetByEmailAsync(string email, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  Task<List<User>> IUserRepository.GetAllAsync(CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}