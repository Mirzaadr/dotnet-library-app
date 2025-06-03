namespace LibraryApp.Api.Models;

public record RegisterRequest(
    string FullName,
    string Email,
    string Password,
    // string UniversityCard,
    int UniversityId
  );

// fullName, email, password, universityCard, universityId