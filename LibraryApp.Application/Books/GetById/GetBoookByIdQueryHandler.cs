using LibraryApp.Application.Abstractions.Data;
using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Books.GetById;

public class GetBooksQueryHandler : IRequestHandler<GetBookByIdQuery, Result<GetBookByIdResponse>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
      _bookRepository = bookRepository;
    }
    
    public async Task<Result<GetBookByIdResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
        if (book is null)
        {
            return (Result<GetBookByIdResponse>)Result<GetBookByIdResponse>.Failure(Error.NotFound("Books.NotFound", "Book not found."));
        }
        return new GetBookByIdResponse
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
        };
    }
}