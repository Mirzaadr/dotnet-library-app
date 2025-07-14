using LibraryApp.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Users.Get;
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<List<GetUsersResponse>>>
{
    private readonly IAppDBContext _context;

    public GetUsersQueryHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result<List<GetUsersResponse>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var userQuery = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(query.SearchTerm))
        {
            userQuery = userQuery.Where(b =>
                b.FullName.Contains(query.SearchTerm) ||
                b.Role.ToString().Contains(query.SearchTerm) ||
                b.Status.ToString().Contains(query.SearchTerm)
            );
        }

        var users = await userQuery
          .OrderBy(b => b.CreatedAt)
          .Skip((query.PageNumber - 1) * query.PageSize)
          .Take(query.PageSize)
          .Select(user => new GetUsersResponse
          {
              Id = user.Id.Value,
              Name = user.FullName,
              Email = user.Email,
              UniversityId = user.UniversityId,
              UniversityCard = user.UniversityCard,
              Status = user.Status.ToString(),
              Role = user.Role.ToString(),
              LastActivityDate = user.LastActivityDate,
              CreatedAt = user.CreatedAt,
              UpdatedAt = user.UpdatedAt
          })
          .ToListAsync(cancellationToken);

        return users;
    }
}