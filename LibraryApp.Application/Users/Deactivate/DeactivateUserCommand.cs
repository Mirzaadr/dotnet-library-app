using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;

namespace LibraryApp.Application.Users.Deactivate;

public record DeactivateUserCommand(Guid Id) : IRequest<Result>;
