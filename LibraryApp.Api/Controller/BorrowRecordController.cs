using System.Security.Claims;
using LibraryApp.Api.Models;
using LibraryApp.Application.Abstractions.Authentication;
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
[Route("api/v{version:apiVersion}/borrows")]
[ApiVersion("1.0")]
public class BorrowRecordController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IUserContext _userContext;

    public BorrowRecordController(ISender mediator, IUserContext userContext)
    {
        _mediator = mediator;
        _userContext = userContext;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> BorrowBook([FromBody] BorrowBookRequest request) // user
    {
        //implement function to reserve book
        var command = new BorrowBookCommand(_userContext.UserId, request.BookId);
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
    [Authorize]
    public async Task<IActionResult> PickupBook(PickupBookCommand command)
    {
        //implement function to change record status to borrow
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
    [Authorize]
    public async Task<IActionResult> ReturnBook(ReturnBookCommand command)
    {
        //implement function to change record status to return
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
        var userId = _userContext.UserId;
        var records = await _mediator.Send(new GetBorrowRecordByUserIdQuery(userId, page, pageSize, null));
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