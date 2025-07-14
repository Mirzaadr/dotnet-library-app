using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Users.UpdateProfile;

public class UpdateUserProfileCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? UniversityCard { get; set; }

    public UpdateUserProfileCommand(Guid id, string? fullName, string? universityCard)
    {
        Id = id;
        FullName = fullName;
        UniversityCard = universityCard;
    }
}
