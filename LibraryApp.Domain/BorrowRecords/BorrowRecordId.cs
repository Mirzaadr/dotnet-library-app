using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Domain.BorrowRecords;

public sealed class BorrowRecordId : ValueObject
{
    public Guid Value { get; }

    private BorrowRecordId(Guid value)
    {
        Value = value;
    }

    public static BorrowRecordId Create(Guid value) => new(value);

    public static BorrowRecordId CreateUnique() => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}