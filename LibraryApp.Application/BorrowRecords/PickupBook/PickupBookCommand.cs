using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.BorrowRecords.PickupBook;

public record PickupBookCommand(Guid userId, Guid recordId) : IRequest<Result>;
