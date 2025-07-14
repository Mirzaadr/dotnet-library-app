using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Users.Deactivate;

public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, Result>
{
    private readonly IAppDBContext _context;

    public DeactivateUserCommandHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        // get current book
        var currentUser = await _context.Users
            .Where(u => u.Id == UserId.Create(request.Id))
            .FirstOrDefaultAsync();

        if (currentUser is null)
        {
            return Result.Failure(UserErrors.NotFound(request.Id));
        }

        if (currentUser.Status == UserStatus.Approved)
        {
            currentUser.Deactivate();
        }
        else if (currentUser.Status == UserStatus.Pending)
        {
            currentUser.Reject();
        }
        
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
