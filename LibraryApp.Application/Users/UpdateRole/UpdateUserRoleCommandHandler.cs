using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Users.UpdateRole;

public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, Result>
{
    private readonly IAppDBContext _context;

    public UpdateUserRoleCommandHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        // get current book
        var currentUser = await _context.Users
            .Where(u => u.Id == UserId.Create(request.Id))
            .FirstOrDefaultAsync();

        if (currentUser is null)
        {
            return Result.Failure(UserErrors.NotFound(request.Id));
        }

        currentUser.ChangeRole(request.role);
        
        try
        {
            _context.Users.Update(currentUser);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.Conflict("500", ex.Message));
        }


    }
}
