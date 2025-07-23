using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Users.Refresh;

public sealed record RefreshUserCommand(string refreshToken) : IRequest<Result<RefreshResponse>>;