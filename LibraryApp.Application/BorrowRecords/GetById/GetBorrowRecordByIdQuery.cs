using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.BorrowRecords.GetById;

public class GetBorrowRecordByIdQuery : IRequest<Result<BorrowRecord>>
{
    public Guid Id { get; }

    public GetBorrowRecordByIdQuery(Guid id)
    {
      Id = id;
    }
}