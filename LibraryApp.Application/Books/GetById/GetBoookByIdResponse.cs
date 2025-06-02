public class GetBookByIdResponse
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
}