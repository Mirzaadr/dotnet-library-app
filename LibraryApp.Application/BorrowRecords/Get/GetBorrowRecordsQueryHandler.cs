using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.BorrowRecords;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.BorrowRecords.Get;

public class GetBorrowRecordsQueryHandler : IRequestHandler<GetBorrowRecordsQuery, Result<List<BorrowRecord>>>
{
    private readonly IAppDBContext _context;

    public GetBorrowRecordsQueryHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result<List<BorrowRecord>>> Handle(GetBorrowRecordsQuery query, CancellationToken cancellationToken)
    {
        var recordsQuery = _context.BorrowRecords.AsQueryable();

        // if (!string.IsNullOrEmpty(query.SearchTerm))
        // {
        //     // recordsQuery = recordsQuery.Where(b => 
        //     //     b.Status.Contains(query.SearchTerm) || 
        //     //     b.Author.Contains(query.SearchTerm));
        // }

        var records = await recordsQuery
          .OrderBy(b => b.CreatedAt)
          .Skip((query.PageNumber - 1) * query.PageSize)
          .Take(query.PageSize)
          .ToListAsync(cancellationToken);

        return records;
    }
}