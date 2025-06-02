using LibraryApp.Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Infrastructure.Persistent.Configurations;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        ConfigureBookTable(builder);
    }

    private void ConfigureBookTable(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(e => e.Id).HasName("books_id_unique");

        builder.ToTable("books");

        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id")
            .ValueGeneratedNever()
            .HasConversion(
              id => id.Value,
              value => BookId.Create(value)
            );
        builder.Property(e => e.Author)
            .HasMaxLength(255)
            .HasColumnName("author");
        builder.Property(e => e.AvailableCopies)
            .HasDefaultValue(0)
            .HasColumnName("available_copies");
        builder.Property(e => e.CoverColor)
            .HasMaxLength(7)
            .HasColumnName("cover_color");
        builder.Property(e => e.CoverUrl).HasColumnName("cover_url");
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp(3) without time zone")
            .HasColumnName("created_at");
        builder.Property(e => e.Description).HasColumnName("description");
        builder.Property(e => e.Genre).HasColumnName("genre");
        builder.Property(e => e.Rating).HasColumnName("rating");
        builder.Property(e => e.Summary)
            .HasColumnType("character varying")
            .HasColumnName("summary");
        builder.Property(e => e.Title)
            .HasMaxLength(255)
            .HasColumnName("title");
        builder.Property(e => e.TotalCopies)
            .HasDefaultValue(1)
            .HasColumnName("total_copies");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("timestamp(3) without time zone")
            .HasColumnName("updated_at");
        builder.Property(e => e.VideoUrl).HasColumnName("video_url");
    }
}