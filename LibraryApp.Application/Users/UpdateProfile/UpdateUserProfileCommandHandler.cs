using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Users.UpdateProfile;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result>
{
    private readonly IAppDBContext _context;

    public UpdateUserProfileCommandHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        // get current book
        var currentUser = await _context.Users
            .Where(u => u.Id == UserId.Create(request.Id))
            .FirstOrDefaultAsync();

        if (currentUser is null)
        {
            return Result.Failure(UserErrors.NotFound(request.Id));
        }

        currentUser.UpdateProfile(
            fullName: request.FullName ?? currentUser.FullName,
            universityCard: request.UniversityCard ?? currentUser.UniversityCard
        );
        
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
