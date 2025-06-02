using DinnerApp.Infrastructure.Persistence;
using LibraryApp.Api.Models;
using LibraryApp.Domain.Books;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controller;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var books = await _context.Books
          .OrderBy(b => b.CreatedAt)
          .Take(10)
          .Select(b => b.Adapt<BookResponse>())
          .ToListAsync();
      return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByID(Guid id)
    {
      var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == BookId.Create(id));
      if (book is null)
      {
        return Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "Book not found",
            detail: $"No book found with ID {id}"
        );
      }
      return Ok(book.Adapt<BookDetailResponse>());
    }
}