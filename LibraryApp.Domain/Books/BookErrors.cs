using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Domain.Books;

public static class BookErrors
{
  public static Error NotFound(Guid bookId) => Error.NotFound(
      "Books.NotFound",
      $"Book with id '{bookId}' was not found.");
}