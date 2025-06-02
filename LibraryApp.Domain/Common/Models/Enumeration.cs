using System.Reflection;

namespace LibraryApp.Domain.Common.Models;

public abstract class Enumeration : IComparable
{
    public string Name { get; private set; }
    public int Id { get; private set; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
            return false;

        return Id == otherValue.Id && GetType().Equals(obj.GetType());
    }

    public override int GetHashCode() => Id.GetHashCode();

    public int CompareTo(object? other)
    {
        if (other is null) return 1;
        return Id.CompareTo(((Enumeration)other).Id);
    }
}
