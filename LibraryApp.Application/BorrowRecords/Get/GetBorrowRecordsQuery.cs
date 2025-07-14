using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.BorrowRecords.Get;

public class GetBorrowRecordsQuery : IRequest<Result<List<BorrowRecord>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }

    public GetBorrowRecordsQuery(int pageNumber, int pageSize, string? searchTerm)
    {
        SearchTerm = searchTerm;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}