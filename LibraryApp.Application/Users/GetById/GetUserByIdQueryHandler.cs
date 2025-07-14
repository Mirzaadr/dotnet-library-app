using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Users.GetById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdResponse>>
{
    private readonly IAppDBContext _context;

    public GetUserByIdQueryHandler(IAppDBContext context)
    {
        _context = context;
    }
    
    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Where(
                b => b.Id == UserId.Create(request.Id)
            ).Select(u => new GetUserByIdResponse
            {
                Id = u.Id.Value,
                Name = u.FullName,
                Email = u.Email,
                Status = u.Status.ToString(),
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            }).FirstOrDefaultAsync();

        if (user is null)
        {
            return Result.Failure<GetUserByIdResponse>(UserErrors.NotFound(request.Id));
        }
        return user;
    }
}