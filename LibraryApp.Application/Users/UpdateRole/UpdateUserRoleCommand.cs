using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;

namespace LibraryApp.Application.Users.UpdateRole;

public record UpdateUserRoleCommand(Guid Id, RoleEnum role) : IRequest<Result>;
