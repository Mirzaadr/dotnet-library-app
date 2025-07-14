using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.BorrowRecords.GetByUserId;

public class GetBorrowRecordByUserIdQuery : IRequest<Result<List<BorrowRecord>>>
{
    public Guid UserId { get; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }

    public GetBorrowRecordByUserIdQuery(Guid userId, int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
    {
        UserId = userId;
        SearchTerm = searchTerm;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}