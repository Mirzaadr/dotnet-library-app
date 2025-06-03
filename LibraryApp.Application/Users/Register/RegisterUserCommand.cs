using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Users.Register;

public sealed record RegisterUserCommand(
    string FullName,
    string Email,
    string Password,
    // string UniversityCard,
    int UniversityId) : IRequest<Result<string>>;

// fullName, email, password, universityCard, universityId