using LibraryApp.Api.Models;
using LibraryApp.Application.BorrowRecords.Get;
using LibraryApp.Application.BorrowRecords.GetById;
using Mapster;
using MediatR;
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
    public async Task<IActionResult> BorrowBook() // user
    {
        //TODO: implement function to reserve book 
        return Ok();
    }

    [HttpPut("{id}/pickup")] // management
    public async Task<IActionResult> PickupBook(Guid id)
    {
        //TODO: implement function to change record status to borrow
        return Ok();
    }

    [HttpPut("{id}/return")] // management
    public async Task<IActionResult> ReturnBook()
    {
        //TODO: implement function to change record status to return
        return Ok();
    }

    [HttpGet("mine")] // user
    public async Task<IActionResult> GetUserRecords()
    {
        //TODO: implement function to return all borrow record for the user
        return Ok();
    }

    [HttpGet] // admin, management
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
        return Ok(record.Adapt<BorrowRecordResponse>());
    }
}