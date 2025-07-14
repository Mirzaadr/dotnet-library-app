using LibraryApp.Domain.Books;
using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.BorrowRecords.PickupBook;

public class BorrowBookCommandHandler : IRequestHandler<PickupBookCommand, Result>
{
    private readonly IAppDBContext _context;

    public BorrowBookCommandHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(PickupBookCommand request, CancellationToken cancellationToken)
    {
        var recordId = BorrowRecordId.Create(request.recordId);
        var record = await _context.BorrowRecords
            .Where(r => r.Id == recordId).FirstOrDefaultAsync(cancellationToken);
        
        if (record is null)
        {
            return Result.Failure<Result>(BorrowRecordErrors.NotFound);
        }

        if (record.UserId != UserId.Create(request.userId))
        {
            return Result.Failure<Result>(UserErrors.Unauthorized());
        }

        try
        {
            record.BorrowBook();
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("500", ex.Message, ErrorType.Failure));
        }
    }
}
