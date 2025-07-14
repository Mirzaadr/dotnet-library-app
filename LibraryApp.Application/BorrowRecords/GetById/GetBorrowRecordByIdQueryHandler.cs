using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.BorrowRecords.GetById;

public class GetBorrowRecordByIdQueryHandler : IRequestHandler<GetBorrowRecordByIdQuery, Result<BorrowRecord>>
{
    private readonly IAppDBContext _context;

    public GetBorrowRecordByIdQueryHandler(IAppDBContext context)
    {
        _context = context;
    }
    
    public async Task<Result<BorrowRecord>> Handle(GetBorrowRecordByIdQuery request, CancellationToken cancellationToken)
    {
        var record = await _context.BorrowRecords.Where(
                b => b.Id == BorrowRecordId.Create(request.Id)
            )
            .FirstOrDefaultAsync(cancellationToken);

        if (record is null)
        {
            return Result.Failure<BorrowRecord>(BorrowRecordErrors.NotFound);
        }
        return record;
    }
}