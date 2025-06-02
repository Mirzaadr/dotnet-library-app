using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Books.Get;

public class GetBooksQuery : IRequest<Result<List<GetBooksResponse>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }

    public GetBooksQuery(int pageNumber, int pageSize, string? searchTerm)
    {
        SearchTerm = searchTerm;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}