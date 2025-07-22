using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Users.Logout;

public sealed record LogoutUserCommand(Guid userId) : IRequest<Result>;