using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Domain.BorrowRecords;

public static class BorrowRecordErrors
{
  public readonly static Error NotFound = Error.NotFound(
      "BorrowRecord.NotFound",
      "Unable to find record");
}