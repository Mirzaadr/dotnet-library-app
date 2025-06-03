using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Books.GetById;

public class GetBooksQueryHandler : IRequestHandler<GetBookByIdQuery, Result<GetBookByIdResponse>>
{
    // private readonly IBookRepository _bookRepository;
    private readonly IAppDBContext _context;

    public GetBooksQueryHandler(IAppDBContext context)
    {
        _context = context;
    }
    
    public async Task<Result<GetBookByIdResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.Where(
                b => b.Id == BookId.Create(request.Id)
            ).Select(book => new GetBookByIdResponse
            {
                Id = book.Id.Value,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Rating = book.Rating,
                CoverUrl = book.CoverUrl,
                CoverColor = book.CoverColor,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies,
                Summary = book.Summary,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt
            }).FirstOrDefaultAsync();

        if (book is null)
        {
            return Result.Failure<GetBookByIdResponse>(BookErrors.NotFound(request.Id));
        }
        return book;
    }
}