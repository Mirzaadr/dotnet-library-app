using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;

namespace LibraryApp.Application.Users.Activate;

public record ActivateUserCommand(Guid Id) : IRequest<Result>;
