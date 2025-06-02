using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Domain.BorrowRecords;

public enum BorrowStatusEnum
{
    BORROWED,
    RETURNED
}

public class BorrowStatus : Enumeration
{
    public static readonly BorrowStatus Borrowed = new(1, "BORROWED", BorrowStatusEnum.BORROWED);
    public static readonly BorrowStatus Returned = new(2, "RETURNED", BorrowStatusEnum.RETURNED);

    public BorrowStatusEnum EnumValue { get; }

    private BorrowStatus(int id, string name, BorrowStatusEnum enumValue) : base(id, name)
    {
        EnumValue = enumValue;
    }

    public static BorrowStatus FromEnum(BorrowStatusEnum e) =>
        GetAll<BorrowStatus>().First(r => r.EnumValue == e);

    public BorrowStatusEnum ToEnum() => EnumValue;

    public static BorrowStatus FromName(string name) =>
        GetAll<BorrowStatus>().FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        ?? throw new ArgumentException($"Invalid status name: {name}");

    public static BorrowStatus FromId(int id) =>
        GetAll<BorrowStatus>().FirstOrDefault(r => r.Id == id)
        ?? throw new ArgumentException($"Invalid status id: {id}");
}

