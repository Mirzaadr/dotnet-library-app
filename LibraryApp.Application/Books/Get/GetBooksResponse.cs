namespace LibraryApp.Application.Books.Get;

public class GetBooksResponse
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
    public string Summary { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}