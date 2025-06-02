namespace LibraryApp.Api.Models;

public class BookResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public int Rating { get; set; }
    public string CoverUrl { get; set; } = null!;
    public string CoverColor { get; set; } = null!;
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public BookResponse(Guid id, string title, string author, string genre, int rating, string coverUrl, string coverColor, int totalCopies, int availableCopies, DateTime? createdAt = null, DateTime? updatedAt = null)
    {
        Id = id;
        Title = title;
        Author = author;
        Genre = genre;
        Rating = rating;
        CoverUrl = coverUrl;
        CoverColor = coverColor;
        TotalCopies = totalCopies;
        AvailableCopies = availableCopies;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    public BookResponse() { }
}

public class BookDetailResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public int Rating { get; set; }
    public string CoverUrl { get; set; } = null!;
    public string CoverColor { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public string VideoUrl { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public BookDetailResponse(Guid id, string title, string author, string genre, int rating, string coverUrl, string coverColor, string description, int totalCopies, int availableCopies, string videoUrl, string summary, DateTime? createdAt = null, DateTime? updatedAt = null)
    {
        Id = id;
        Title = title;
        Author = author;
        Genre = genre;
        Rating = rating;
        CoverUrl = coverUrl;
        CoverColor = coverColor;
        Description = description;
        TotalCopies = totalCopies;
        AvailableCopies = availableCopies;
        VideoUrl = videoUrl;
        Summary = summary;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    public BookDetailResponse() { }
}