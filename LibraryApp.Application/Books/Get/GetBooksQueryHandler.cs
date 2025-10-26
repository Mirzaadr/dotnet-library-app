using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Books.Get;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, Result<PagedList<GetBooksResponse>>>
{
    private readonly IAppDBContext _context;

    public GetBooksQueryHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedList<GetBooksResponse>>> Handle(GetBooksQuery query, CancellationToken cancellationToken)
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

        var pagedResponses = await PagedList<GetBooksResponse>.CreateAsync(
            bookQuery.Select(book => new GetBooksResponse
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
            }),
            query.PageNumber,
            query.PageSize
        );

        return pagedResponses;

        // return books;
    }
}