using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Books.Delete;

public record DeleteBookCommand(Guid Id) : IRequest<Result>;
