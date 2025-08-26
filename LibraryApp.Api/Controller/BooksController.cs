using LibraryApp.Api.Models;
using LibraryApp.Application.Books.Create;
using LibraryApp.Application.Books.Delete;
using LibraryApp.Application.Books.Get;
using LibraryApp.Application.Books.Update;
using LibraryApp.Domain.Common.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controller;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class BooksController : ControllerBase
{
    private readonly ISender _mediator;

    public BooksController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet] // public
    public async Task<IActionResult> GetAllBooks(int page = 1, int pageSize = 10)
    {
        var query = new GetBooksQuery(page, pageSize, null);
        var books = await _mediator.Send(query);
        return Ok(books.Value.Adapt<List<BookResponse>>());
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
        return Ok(bookResult.Value.Adapt<BookDetailResponse>());
    }

    [HttpPost] // admin, management
    [Authorize]
    public async Task<IActionResult> AddBook([FromBody] CreateBookCommand request)
    {
        //implement function to add book
        var result = await _mediator.Send(request);
        if (result.IsFailure)
        {
            var statusCode = result.Error.Type switch
            {
                ErrorType.Validation or ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Description
            );
        }
        return Created();
    }

    [HttpPut("{id}")] // admin, management
    [Authorize]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookCommand request)
    {
        //implement function to update book
        request.Id = id;
        var result = await _mediator.Send(request);
        if (result.IsFailure)
        {
            var statusCode = result.Error.Type switch
            {
                ErrorType.Validation or ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Description
            );
        }
        return NoContent();
    }

    [HttpDelete("{id}")] // admin
    [Authorize]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        //implement function to delete book 
        var result = await _mediator.Send(new DeleteBookCommand(id));
        if (result.IsFailure)
        {
            var statusCode = result.Error.Type switch
            {
                ErrorType.Validation or ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Description
            );
        }
        return NoContent();
    }
}