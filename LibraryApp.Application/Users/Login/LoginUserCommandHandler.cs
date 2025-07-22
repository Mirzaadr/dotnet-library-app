using LibraryApp.Application.Abstractions.Authentication;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace LibraryApp.Application.Users.Login;

internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginResponse>>
{
    private readonly IAppDBContext _context;
    private readonly IPasswordHasher _hasher;
    private readonly ITokenProvider __tokenProvider;

    public LoginUserCommandHandler(IAppDBContext context, IPasswordHasher hasher, ITokenProvider tokenProvider)
    {
        _context = context;
        _hasher = hasher;
        __tokenProvider = tokenProvider;
    }

    public async Task<Result<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // check user exist
        User? user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginResponse>(UserErrors.NotFoundByEmail);
        }

        bool verified = _hasher.Verify(request.Password, user.Password);

        if (!verified)
        {
            return Result.Failure<LoginResponse>(UserErrors.NotFoundByEmail);
        }

        string token = __tokenProvider.Generate(user);
        var refreshToken = new RefreshToken(
            __tokenProvider.GenerateRefreshToken(),
            user.Id,
            DateTime.Now.AddDays(7) // Set expiration to 7 days
        );

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new LoginResponse(token, refreshToken.Token);
    }
}