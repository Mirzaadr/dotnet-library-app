using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Users.Get;

public class GetUsersQuery : IRequest<Result<List<GetUsersResponse>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }

    public GetUsersQuery(int pageNumber, int pageSize, string? searchTerm)
    {
        SearchTerm = searchTerm;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}