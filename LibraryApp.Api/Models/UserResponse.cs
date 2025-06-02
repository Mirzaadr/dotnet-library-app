namespace LibraryApp.Api.Models;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Status { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public UserResponse(Guid id, string name, string email, string? status, DateTime? createdAt, DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        Email = email;
        Status = status ?? "unknown";
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}

public class UserDetailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int UniversityId { get; set; }
    public string UniversityCard { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public UserDetailResponse(
      Guid id, 
      string name, 
      string email,
      int universityId,
      string universityCard,
      string? status,
      string? role,
      DateTime? createdAt, 
      DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        Email = email;
        UniversityId= universityId;
        UniversityCard= universityCard;
        Status= status ?? "unknown";
        Role= role ?? "unknown";
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}