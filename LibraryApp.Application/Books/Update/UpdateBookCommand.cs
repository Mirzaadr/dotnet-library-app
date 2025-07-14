using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.Books.Update;

public class UpdateBookCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Genre { get; set; }
    public int Rating { get; set; }
    public string? CoverUrl { get; set; }
    public string? CoverColor { get; set; }
    public string? Description { get; set; }
    public string? VideoUrl { get; set; }
    public string? Summary { get; set; }
}
