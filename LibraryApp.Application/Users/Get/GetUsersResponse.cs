namespace LibraryApp.Application.Users.Get;

public class GetUsersResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int UniversityId { get; set; }
    public string UniversityCard { get; set; } = string.Empty;
    public string? Status { get; set; } = string.Empty;
    public string? Role { get; set; } = string.Empty;
    public DateTime? LastActivityDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}