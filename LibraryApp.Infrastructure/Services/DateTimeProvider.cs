using LibraryApp.Application.Abstractions.Services;

namespace LibraryApp.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
  public DateTime UtcNow => DateTime.UtcNow;
}