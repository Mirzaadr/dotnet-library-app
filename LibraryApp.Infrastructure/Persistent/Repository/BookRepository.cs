using DinnerApp.Infrastructure.Persistence;
using LibraryApp.Application.Abstractions.Data;
using LibraryApp.Domain.Books;
using LibraryApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Persistent.Repository;

public class BookRepository : IBookRepository
{
  private readonly AppDbContext _context;

  public BookRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
      return await _context.Books.SingleOrDefaultAsync(b => b.Id == BookId.Create(id), cancellationToken);
  }

  public async Task<List<Book>> GetAllAsync(CancellationToken cancellationToken)
  {
      return await _context.Books.ToListAsync(cancellationToken);
  }

  public async Task<List<Book>> SearchAsync(int pageNumber, int pageSize, string? searchTerm, CancellationToken cancellationToken = default)
  {
      var query = _context.Books.AsQueryable();
      if (!string.IsNullOrEmpty(searchTerm))
      {
          query = query.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
      }
      return await query
          .OrderBy(b => b.CreatedAt)
          .Skip((pageNumber - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync(cancellationToken);
  }

  public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
  {
      await _context.Books.AddAsync(book, cancellationToken);
      await _context.SaveChangesAsync(cancellationToken);
  }

  public async Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
  {
      _context.Books.Update(book);
      await _context.SaveChangesAsync(cancellationToken);
  }
  
  public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
      var book = await GetByIdAsync(id, cancellationToken);
      if (book is not null)
      {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);
    }
  }
}