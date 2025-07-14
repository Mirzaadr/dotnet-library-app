namespace LibraryApp.Api.Models;

public record BorrowBookRequest(
    Guid BookId
  );