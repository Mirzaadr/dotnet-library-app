using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;

namespace LibraryApp.Application.BorrowRecords.BorrowBook;

public record BorrowBookCommand(Guid UserId, Guid BookId) : IRequest<Result>;
