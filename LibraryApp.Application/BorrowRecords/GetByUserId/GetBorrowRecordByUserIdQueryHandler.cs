using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.BorrowRecords.GetByUserId;

public class GetBorrowRecordByUserIdQueryHandler : IRequestHandler<GetBorrowRecordByUserIdQuery, Result<List<BorrowRecord>>>
{
    private readonly IAppDBContext _context;

    public GetBorrowRecordByUserIdQueryHandler(IAppDBContext context)
    {
        _context = context;
    }
    
    public async Task<Result<List<BorrowRecord>>> Handle(GetBorrowRecordByUserIdQuery query, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(query.UserId);
        var currentUser = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
        if (currentUser is null)
        {
            return Result.Failure<List<BorrowRecord>>(BorrowRecordErrors.NotFound);
        }

        var recordsQuery = _context.BorrowRecords.AsQueryable();
        recordsQuery = recordsQuery.Where<BorrowRecord>(r => r.UserId == UserId.Create(query.UserId));

        if (!string.IsNullOrEmpty(query.SearchTerm))
        {
            recordsQuery = recordsQuery.Where(b => 
                b.Status.ToString().Contains(query.SearchTerm));
        }

        var records = await recordsQuery
          .OrderBy(b => b.CreatedAt)
          .Skip((query.PageNumber - 1) * query.PageSize)
          .Take(query.PageSize)
          .ToListAsync(cancellationToken);

        if (records is null)
        {
            return Result.Failure<List<BorrowRecord>>(BorrowRecordErrors.NotFound);
        }
        return records;
    }
}