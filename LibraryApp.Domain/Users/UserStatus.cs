using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Domain.Users;

public enum StatusEnum
{
    PENDING,
    APPROVED,
    REJECTED,
    BLOCKED
}
public class UserStatus : Enumeration
{
    public static readonly UserStatus Pending = new(1, "PENDING", StatusEnum.PENDING);
    public static readonly UserStatus Approved = new(2, "APPROVED", StatusEnum.APPROVED);
    public static readonly UserStatus Rejected = new(3, "REJECTED", StatusEnum.REJECTED);
    public static readonly UserStatus Rejected = new(4, "BLOCKED", StatusEnum.BLOCKED);

    public StatusEnum EnumValue { get; }

    private UserStatus(int id, string name, StatusEnum enumValue) : base(id, name)
    {
        EnumValue = enumValue;
    }

    public static UserStatus FromEnum(StatusEnum e) =>
        GetAll<UserStatus>().First(r => r.EnumValue == e);

    public StatusEnum ToEnum() => EnumValue;

    public static UserStatus FromName(string name) =>
        GetAll<UserStatus>().FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        ?? throw new ArgumentException($"Invalid status name: {name}");

    public static UserStatus FromId(int id) =>
        GetAll<UserStatus>().FirstOrDefault(r => r.Id == id)
        ?? throw new ArgumentException($"Invalid status id: {id}");
}

