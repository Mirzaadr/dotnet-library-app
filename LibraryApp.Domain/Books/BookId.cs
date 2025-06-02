using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Domain.Books;

public sealed class BookId : ValueObject
{
    public Guid Value { get; }

    private BookId(Guid value)
    {
        Value = value;
    }

    public static BookId Create(Guid value) => new(value);

    public static BookId CreateUnique() => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}