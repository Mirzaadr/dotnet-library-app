using LibraryApp.Application.Abstractions.Authentication;
using LibraryApp.Application.Users.Login;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace LibraryApp.Application.Users.Register;

internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<string>>
{
    private readonly IAppDBContext _context;
    private readonly IPasswordHasher _hasher;
    private readonly ITokenProvider _tokenProvider;

    public RegisterUserCommandHandler(IAppDBContext context, IPasswordHasher hasher, ITokenProvider tokenProvider)
    {
        _context = context;
        _hasher = hasher;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<string>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        // check user exist
        bool isExist = await _context.Users
            .AsNoTracking()
            .AnyAsync(u =>
                u.Email == command.Email || u.UniversityId == command.UniversityId,
                cancellationToken);

        if (isExist)
        {
            return Result.Failure<string>(UserErrors.EmailNotUnique);
        }

        var user = new User(
            UserId.CreateUnique(),
            command.FullName,
            command.Email,
            command.UniversityId,
            string.Empty,
            _hasher.Hash(command.Password)
        );

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user.Id.Value.ToString();
    }
}