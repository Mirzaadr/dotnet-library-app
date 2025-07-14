using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Users.GetById;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<GetUserByIdResponse>>;