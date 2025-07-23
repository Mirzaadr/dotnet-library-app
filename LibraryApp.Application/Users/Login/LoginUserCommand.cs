using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;