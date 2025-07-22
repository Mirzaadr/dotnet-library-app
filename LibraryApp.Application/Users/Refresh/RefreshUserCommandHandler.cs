using LibraryApp.Application.Abstractions.Authentication;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace LibraryApp.Application.Users.Refresh;

internal sealed class RefreshUserCommandHandler : IRequestHandler<RefreshUserCommand, Result<RefreshResponse>>
{
    private readonly IAppDBContext _context;
    private readonly ITokenProvider __tokenProvider;

    public RefreshUserCommandHandler(IAppDBContext context, ITokenProvider tokenProvider)
    {
        _context = context;
        __tokenProvider = tokenProvider;
    }

    public async Task<Result<RefreshResponse>> Handle(RefreshUserCommand request, CancellationToken cancellationToken)
    {
        // check refresh token exist
        RefreshToken? refreshToken = await _context.RefreshTokens
            // .Where(rt => rt.Token == request.refreshToken)
            .FirstOrDefaultAsync(r => r.Token == request.refreshToken, cancellationToken);
        if (refreshToken is null || !refreshToken.IsActive)
        {
            return Result.Failure<RefreshResponse>(UserErrors.InvalidRefreshToken);
        }

        User? user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == refreshToken.UserId, cancellationToken);
        
        if (user is null)
        {
            return Result.Failure<RefreshResponse>(UserErrors.NotFound(refreshToken.UserId.Value));
        }

        string accessToken = __tokenProvider.Generate(user);
        refreshToken.Update(
            __tokenProvider.GenerateRefreshToken(),
            DateTime.Now.AddDays(7) // Set expiration to 7 days
        );

        _context.RefreshTokens.Update(refreshToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new RefreshResponse(accessToken, refreshToken.Token);
    }
}