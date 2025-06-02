using LibraryApp.Application.Abstractions.Data;
using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Books.Get;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, Result<List<GetBooksResponse>>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
      _bookRepository = bookRepository;
    }
    
    public async Task<Result<List<GetBooksResponse>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.SearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, cancellationToken);
        return books.Select(book => new GetBooksResponse
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
        }).ToList();
    }
}