using System;
using System.Collections.Generic;
using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Domain.Books;

public partial class Book : AggregateRoot<BookId>
{
    public string Title { get; private set; } = null!;
    public string Author { get; private set; } = null!;
    public string Genre { get; private set; } = null!;
    public int Rating { get; private set; }
    public string CoverUrl { get; private set; } = null!;
    public string CoverColor { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int TotalCopies { get; private set; }
    public int AvailableCopies { get; private set; }
    public string VideoUrl { get; private set; } = null!;
    public string Summary { get; private set; } = null!;
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Book() { }

    public Book(
        BookId id,
        string title,
        string author,
        string genre,
        int rating,
        string coverUrl,
        string coverColor,
        string description,
        int totalCopies,
        string videoUrl,
        string summary) : base(id)
    {
        Title = title;
        Author = author;
        Genre = genre;
        Rating = rating;
        CoverUrl = coverUrl;
        CoverColor = coverColor;
        Description = description;
        TotalCopies = totalCopies;
        AvailableCopies = totalCopies;
        VideoUrl = videoUrl;
        Summary = summary;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    // ✅ Update details (e.g., when admin edits metadata)
    public void UpdateDetails(
        string title,
        string author,
        string genre,
        int rating,
        string coverUrl,
        string coverColor,
        string description,
        string videoUrl,
        string summary)
    {
        Title = title;
        Author = author;
        Genre = genre;
        Rating = rating;
        CoverUrl = coverUrl;
        CoverColor = coverColor;
        Description = description;
        VideoUrl = videoUrl;
        Summary = summary;
        // UpdatedAt = DateTime.UtcNow;
    }

    // ✅ Inventory methods
    public void AddCopies(int count)
    {
        if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
        TotalCopies += count;
        AvailableCopies += count;
        // UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveCopies(int count)
    {
        if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (count > AvailableCopies) throw new InvalidOperationException("Cannot remove more copies than are available.");
        TotalCopies -= count;
        AvailableCopies -= count;
        // UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsBorrowed()
    {
        if (AvailableCopies <= 0) throw new InvalidOperationException("No copies available to borrow.");
        AvailableCopies--;
        // UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsReturned()
    {
        if (AvailableCopies >= TotalCopies) throw new InvalidOperationException("All copies are already returned.");
        AvailableCopies++;
        // UpdatedAt = DateTime.UtcNow;
    }
}
