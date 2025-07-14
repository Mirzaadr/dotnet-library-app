using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Books.Create;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result>
{
    private readonly IAppDBContext _context;

    public CreateBookCommandHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {        
        // create new book
        Book book = new Book(
            BookId.CreateUnique(),
            request.Title,
            request.Author,
            request.Genre,
            request.Rating,
            request.CoverUrl,
            request.CoverColor,
            request.Description,
            request.TotalCopies,
            request.VideoUrl,
            request.Summary
        );
        try
        {
            await _context.Books.AddAsync(book, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("500", ex.Message, ErrorType.Failure));
        }


    }
}
