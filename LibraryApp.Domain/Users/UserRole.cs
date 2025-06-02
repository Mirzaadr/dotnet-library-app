using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Domain.Users;

public enum RoleEnum
{
    USER,
    ADMIN
}

public class UserRole : Enumeration
{
    public static readonly UserRole Admin = new(1, "ADMIN", RoleEnum.ADMIN);
    public static readonly UserRole User = new(2, "USER", RoleEnum.USER);

    public RoleEnum EnumValue { get; }

    private UserRole(int id, string name, RoleEnum enumValue) : base(id, name)
    {
        EnumValue = enumValue;
    }

    public static UserRole FromEnum(RoleEnum e) =>
        GetAll<UserRole>().First(r => r.EnumValue == e);

    public RoleEnum ToEnum() => EnumValue;

    // private UserRole(int id, string name) : base(id, name) { }

    public static UserRole FromName(string name) =>
        GetAll<UserRole>().FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        ?? throw new ArgumentException($"Invalid role name: {name}");

    public static UserRole FromId(int id) =>
        GetAll<UserRole>().FirstOrDefault(r => r.Id == id)
        ?? throw new ArgumentException($"Invalid role id: {id}");
}

