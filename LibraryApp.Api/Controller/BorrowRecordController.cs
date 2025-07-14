using System.Security.Claims;
using LibraryApp.Api.Models;
using LibraryApp.Application.BorrowRecords.BorrowBook;
using LibraryApp.Application.BorrowRecords.Get;
using LibraryApp.Application.BorrowRecords.GetById;
using LibraryApp.Application.BorrowRecords.GetByUserId;
using LibraryApp.Application.BorrowRecords.PickupBook;
using LibraryApp.Application.BorrowRecords.ReturnBook;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controller;

[ApiController]
[Route("api/v1/borrows")]
public class BorrowRecordController : ControllerBase
{
    private readonly ISender _mediator;

    public BorrowRecordController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> BorrowBook([FromBody] BorrowBookRequest request) // user
    {
        //implement function to reserve book
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Unauthorized",
                detail: "User identifier not found."
            );
        }
        var command = new BorrowBookCommand(Guid.Parse(userId), request.BookId);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal Server Error",
                detail: $"Something happened"
            );
        }
        return Created();
    }

    [HttpPut("{id}/pickup")] // management
    public async Task<IActionResult> PickupBook(Guid id)
    {
        //implement function to change record status to borrow
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Unauthorized",
                detail: "User identifier not found."
            );
        }
        var command = new PickupBookCommand(Guid.Parse(userId), id);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal Server Error",
                detail: $"Something happened"
            );
        }

        return NoContent();
    }

    [HttpPut("{id}/return")] // management
    public async Task<IActionResult> ReturnBook(Guid id)
    {
        //implement function to change record status to return
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Unauthorized",
                detail: "User identifier not found."
            );
        }
        var command = new ReturnBookCommand(Guid.Parse(userId), id);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal Server Error",
                detail: $"Something happened"
            );
        }

        return NoContent();
    }

    [HttpGet("mine")] // user
    [Authorize]
    public async Task<IActionResult> GetUserRecords(int page = 1, int pageSize = 10)
    {
        //implement function to return all borrow record for the user
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Unauthorized",
                detail: "User identifier not found."
            );
        }
        var records = await _mediator.Send(new GetBorrowRecordByUserIdQuery(Guid.Parse(userId), page, pageSize, null));
        if (records.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "User not found",
                detail: $"No record found with UserID {userId}"
            );
        }
        return Ok(records.Value.Adapt<List<BorrowRecordResponse>>());
    }

    [HttpGet] // admin, management
    [Authorize]
    public async Task<IActionResult> GetAllRecords(int page = 1, int pageSize = 10)
    {
        //implement function to get all books record with pagination
        var records = await _mediator.Send(new GetBorrowRecordsQuery(page, pageSize, null));
        if (records.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Unable to get data",
                detail: "We are unable to get all data. try again later."
            );
        }
        return Ok(records.Value.Adapt<List<BorrowRecordResponse>>());
    }

    [HttpGet("{id}")] // any
    public async Task<IActionResult> GetRecordById(Guid id)
    {
        //implement function to get record by its id
        var record = await _mediator.Send(new GetBorrowRecordByIdQuery(id));
        if (record.IsFailure)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Borrow Record not found",
                detail: $"No record found with ID {id}"
            );
        }
        return Ok(record.Value.Adapt<BorrowRecordResponse>());
    }
}