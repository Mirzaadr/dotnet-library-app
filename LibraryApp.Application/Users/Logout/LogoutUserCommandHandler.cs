using LibraryApp.Application.Abstractions.Authentication;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace LibraryApp.Application.Users.Logout;

internal sealed class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, Result>
{
    private readonly IAppDBContext _context;

    public LogoutUserCommandHandler(IAppDBContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        // check user exist
        User? user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == UserId.Create(request.userId), cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFoundByEmail);
        }

        // check refresh token exist
        RefreshToken? refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == user.Id, cancellationToken);

        if (refreshToken is not null)
        {
            refreshToken.Revoke();
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}