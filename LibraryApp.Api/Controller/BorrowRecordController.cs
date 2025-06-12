using System.Threading.Tasks;
using DinnerApp.Infrastructure.Persistence;
using LibraryApp.Api.Models;
using LibraryApp.Domain.BorrowRecords;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controller;

[ApiController]
[Route("api/v1/borrows")]
public class BorrowRecordController : ControllerBase
{
    private readonly AppDbContext _context;

    public BorrowRecordController(AppDbContext context)
    {
        _context = context;
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
        var records = await _context.BorrowRecords.OrderBy(b => b.CreatedAt).Take(10).ToListAsync();
        return Ok(records.Adapt<List<BorrowRecordResponse>>());
    }

    [HttpGet] // admin, management
    public async Task<IActionResult> GetAllRecords()
    {
        //TODO: implement function to get all books record with pagination
        var records = await _context.BorrowRecords.OrderBy(b => b.CreatedAt).Take(10).ToListAsync();
        return Ok(records.Adapt<List<BorrowRecordResponse>>());
    }

    [HttpGet("{id}")] // any
    public async Task<IActionResult> GetRecordById(Guid id)
    {
        //TODO: implement function to get record by its id
        var record = await _context.BorrowRecords.FirstOrDefaultAsync(r => r.Id == BorrowRecordId.Create(id));
        if (record is null)
        {
            return NotFound();
        }
        return Ok(record.Adapt<BorrowRecordResponse>());
    }
}