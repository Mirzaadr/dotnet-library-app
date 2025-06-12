using DinnerApp.Infrastructure.Persistence;
using LibraryApp.Api.Models;
using LibraryApp.Application.Books.Get;
using LibraryApp.Domain.Books;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class BooksController : ControllerBase
{
  private readonly ISender _mediator;

  public BooksController(ISender mediator)
  {
    _mediator = mediator;
  }

  [HttpGet] // public
  // [Authorize]
  public async Task<IActionResult> GetAllBooks(int page = 1, int pageSize = 10)
  {
    var query = new GetBooksQuery(page, pageSize, null);
    var books = await _mediator.Send(query);
    return Ok(books.Value);
  }

  [HttpGet("{id}")] // public
  public async Task<IActionResult> GetBookByID(Guid id)
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

  [HttpPost] // admin, management
  public async Task<IActionResult> AddBook()
  {
    //TODO: implement function to add book
    await Task.CompletedTask;
    return Ok();
  }

  [HttpPut("{id}")] // admin, management
  public async Task<IActionResult> UpdateBook(Guid id)
  {
    //TODO: implement function to update book
    await Task.CompletedTask;
    return Ok(id);
  }

  [HttpDelete("{id}")] // admin
  public async Task<IActionResult> DeleteBook(Guid id)
  {
    //TODO: implement function to delete book 
    await Task.CompletedTask;
    return Ok(id);
  }
}