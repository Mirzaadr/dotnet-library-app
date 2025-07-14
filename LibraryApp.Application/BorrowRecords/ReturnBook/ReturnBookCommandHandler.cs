using LibraryApp.Domain.Books;
using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.BorrowRecords.ReturnBook;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Result>
{
    private readonly IAppDBContext _context;

    public ReturnBookCommandHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
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
            record.ReturnBook();
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("500", ex.Message, ErrorType.Failure));
        }
    }
}
