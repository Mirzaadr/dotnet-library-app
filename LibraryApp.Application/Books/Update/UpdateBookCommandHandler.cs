using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Books.Update;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result>
{
    private readonly IAppDBContext _context;

    public UpdateBookCommandHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        // get current book
        var currentBook = await _context.Books
            .Where(b => b.Id == BookId.Create(request.Id))
            .FirstOrDefaultAsync();

        if (currentBook is null)
        {
            return Result.Failure(UserErrors.NotFound(request.Id));
        }

        currentBook.UpdateDetails(
            title: request.Title ?? currentBook.Title,
            author: request.Author ?? currentBook.Author,
            genre: request.Summary ?? currentBook.Summary,
            rating: request.Rating,
            coverUrl: request.CoverUrl ?? currentBook.CoverUrl,
            coverColor: request.CoverColor ?? currentBook.CoverColor,
            description: request.Description ?? currentBook.Description,
            videoUrl: request.VideoUrl ?? currentBook.VideoUrl,
            summary: request.Summary ?? currentBook.Summary
        );
        
        try
        {
            _context.Books.Update(currentBook);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("500", ex.Message, ErrorType.Failure));
        }


    }
}
