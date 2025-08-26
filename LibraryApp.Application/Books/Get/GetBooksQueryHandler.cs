using LibraryApp.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Books.Get;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, Result<List<GetBooksResponse>>>
{
    private readonly IAppDBContext _context;

    public GetBooksQueryHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result<List<GetBooksResponse>>> Handle(GetBooksQuery query, CancellationToken cancellationToken)
    {
        var bookQuery = _context.Books.AsQueryable();

        if (!string.IsNullOrEmpty(query.SearchTerm))
        {
            var searchTerms = query.SearchTerm
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(term => term.ToLower())
                .ToList();
            
            bookQuery = bookQuery.Where(book =>
                searchTerms.All(term => book.Title.ToLower().Contains(term)) ||
                searchTerms.All(term => book.Author.ToLower().Contains(term)) ||
                searchTerms.All(term => book.Genre.ToLower().Contains(term))
            );
        }

        var books = await bookQuery
          .OrderBy(b => b.CreatedAt)
          .Skip((query.PageNumber - 1) * query.PageSize)
          .Take(query.PageSize)
          .Select(book => new GetBooksResponse
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
          })
          .ToListAsync(cancellationToken);

        return books;
    }
}