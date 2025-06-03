using DinnerApp.Infrastructure.Persistence;
using LibraryApp.Api.Models;
using LibraryApp.Application.Books.Get;
using LibraryApp.Domain.Books;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controller;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly ISender _mediator;

    public BooksController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        var query = new GetBooksQuery(page, pageSize, null);
        var books = await _mediator.Send(query);
        return Ok(books.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByID(Guid id)
    {
      // var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == BookId.Create(id));
      var query = new GetBookByIdQuery(id);
      var bookResult = await _mediator.Send(query);
      if (bookResult.IsFailure)
      {
        return Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "Book not found",
            detail: $"No book found with ID {id}"
        );
      }
      return Ok(bookResult.Value);
    }
}