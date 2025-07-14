using LibraryApp.Domain.Books;
using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.BorrowRecords.BorrowBook;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Result>
{
    private readonly IAppDBContext _context;

    public BorrowBookCommandHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        UserId userId = UserId.Create(request.UserId);
        BookId bookId = BookId.Create(request.BookId);

        // check if user exist
        var isUserExist = (
            await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync(cancellationToken)
            ) is not null;

        if (!isUserExist)
        {
            return Result.Failure<Result>(UserErrors.NotFound(request.UserId));
        }

        // check if book exist
        var isBookExist = (
            await _context.Books
                .Where(u => u.Id == bookId)
                .FirstOrDefaultAsync(cancellationToken)
            ) is not null;
        
        
        if (!isBookExist)
        {
            return Result.Failure<Result>(BookErrors.NotFound(request.BookId));
        }
        
        // create new record
        BorrowRecord record = new BorrowRecord(
            BorrowRecordId.CreateUnique(),
            userId,
            bookId,
            DateOnly.FromDateTime(DateTime.Now.AddDays(30))
        );
        try
        {
            await _context.BorrowRecords.AddAsync(record, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("500", ex.Message, ErrorType.Failure));
        }


    }
}
