using System.Threading.Tasks;
using DinnerApp.Infrastructure.Persistence;
using LibraryApp.Api.Models;
using LibraryApp.Domain.BorrowRecords;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Api.Controller;

[ApiController]
[Route("borrows")]
public class BorrowRecordController : ControllerBase
{
    private readonly AppDbContext _context;

    public BorrowRecordController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("records")]
    public async Task<IActionResult> GetRecords()
    {
        var records = await _context.BorrowRecords.OrderBy(b => b.CreatedAt).Take(10).ToListAsync();
        return Ok(records.Adapt<List<BorrowRecordResponse>>());
    }

    [HttpGet("records/{id}")]
    public async Task<IActionResult> GetRecordById(Guid id)
    {
        var record = await _context.BorrowRecords.FirstOrDefaultAsync(r => r.Id == BorrowRecordId.Create(id));
        if (record is null)
        {
            return NotFound();
        }
        return Ok(record.Adapt<BorrowRecordResponse>());
    }
}