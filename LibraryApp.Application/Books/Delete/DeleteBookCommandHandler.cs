using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Books.Delete;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
{
    private readonly IAppDBContext _context;

    public DeleteBookCommandHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        // get current book
        var currentBook = await _context.Books
            .Where(b => b.Id == BookId.Create(request.Id))
            .FirstOrDefaultAsync();

        if (currentBook is null)
        {
            return Result.Failure(UserErrors.NotFound(request.Id));
        }
        
        try
        {
            _context.Books.Remove(currentBook);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("500", ex.Message, ErrorType.Failure));
        }


    }
}
