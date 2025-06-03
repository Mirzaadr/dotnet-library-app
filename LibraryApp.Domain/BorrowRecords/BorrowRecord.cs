using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryApp.Domain.Books;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;

namespace LibraryApp.Domain.BorrowRecords;

public partial class BorrowRecord : AggregateRoot<BorrowRecordId>
{
    public UserId UserId { get; private set; }
    public BookId BookId { get; private set; }
    public DateTime BorrowDate { get; private set; }
    public DateOnly DueDate { get; private set; }
    public DateOnly? ReturnDate { get; private set; }
    public BorrowStatusEnum StatusValue { get; private set; }
    [NotMapped]
    public BorrowStatus Status => BorrowStatus.FromEnum(StatusValue);
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // 🔒 EF Core constructor
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private BorrowRecord() { }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    // ✅ Public constructor for creating a new borrow record
    public BorrowRecord(BorrowRecordId id, UserId userId, BookId bookId, DateOnly dueDate): base(id)
    {
        UserId = userId;
        BookId = bookId;
        BorrowDate = DateTime.UtcNow;
        DueDate = dueDate;
        StatusValue = BorrowStatusEnum.BORROWED;
        // CreatedAt = DateTime.UtcNow;
    }

    // ✅ Mark as returned
    public void ReturnBook()
    {
        if (StatusValue != BorrowStatusEnum.BORROWED)
            throw new InvalidOperationException("Cannot return a book that is not currently borrowed.");

        ReturnDate = DateOnly.FromDateTime(DateTime.UtcNow);
        StatusValue = BorrowStatusEnum.RETURNED;
        // UpdatedAt = DateTime.UtcNow;
    }

    // ✅ Mark as overdue
    // public void MarkAsOverdue()
    // {
    //     if (StatusValue != BorrowStatusEnum.BORROWED)
    //         throw new InvalidOperationException("Only borrowed records can become overdue.");

    //     if (DueDate < DateOnly.FromDateTime(DateTime.UtcNow))
    //     {
    //         StatusValue = BorrowStatusEnum.OVERDUE;
    //         UpdatedAt = DateTime.UtcNow;
    //     }
    // }

    // ✅ Extend due date
    public void ExtendDueDate(DateOnly newDueDate)
    {
        if (StatusValue != BorrowStatusEnum.BORROWED)
            throw new InvalidOperationException("Can only extend due dates for active borrow records.");

        if (newDueDate <= DueDate)
            throw new ArgumentException("New due date must be later than current due date.");

        DueDate = newDueDate;
        // UpdatedAt = DateTime.UtcNow;
    }
}
