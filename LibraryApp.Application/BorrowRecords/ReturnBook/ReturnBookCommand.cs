using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.BorrowRecords.ReturnBook;

public record ReturnBookCommand(Guid userId, Guid recordId) : IRequest<Result>;
