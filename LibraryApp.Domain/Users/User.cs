using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Domain.Users;

public partial class User : AggregateRoot<UserId>
{
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public int UniversityId { get; private set; }
    public string Password { get; private set; } = null!;
    public string UniversityCard { get; private set; } = null!;
    public DateTime? LastActivityDate { get; private set; }
    public StatusEnum StatusValue { get; private set; }
    [NotMapped]
    public UserStatus Status => UserStatus.FromEnum(StatusValue);
    public RoleEnum RoleValue { get; private set; }
    [NotMapped]
    public UserRole Role => UserRole.FromEnum(RoleValue);
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private User() {}

    // ✅ Public constructor for user creation
    public User(
        UserId id,
        string fullName,
        string email,
        int universityId,
        string universityCard,
        string hashedPassword,
        RoleEnum role = RoleEnum.USER) : base(id)
    {
        FullName = fullName;
        Email = email;
        UniversityId = universityId;
        Password = hashedPassword; // Should be hashed already
        UniversityCard = universityCard;
        RoleValue = role;
        StatusValue = StatusEnum.PENDING;
        // CreatedAt = DateTime.UtcNow;
    }

    // ✅ Update profile
    public void UpdateProfile(string fullName, string universityCard)
    {
        FullName = fullName;
        UniversityCard = universityCard;
        // UpdatedAt = DateTime.UtcNow;
    }

    // ✅ Change password
    public void ChangePassword(string hashedPassword)
    {
        Password = hashedPassword;
        // UpdatedAt = DateTime.UtcNow;
    }

    // ✅ Mark last activity
    public void UpdateLastActivity()
    {
        LastActivityDate = DateTime.Now;
    }

    // ✅ Change role (admin function)
    public void ChangeRole(RoleEnum newRole)
    {
        RoleValue = newRole;
        // UpdatedAt = DateTime.UtcNow;
    }

    // ✅ Activate/deactivate account
    public void Deactivate()
    {
        if (StatusValue == StatusEnum.REJECTED) return;
        StatusValue = StatusEnum.REJECTED;
        // UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (StatusValue == StatusEnum.APPROVED) return;
        StatusValue = StatusEnum.APPROVED;
        // UpdatedAt = DateTime.UtcNow;
    }
}
