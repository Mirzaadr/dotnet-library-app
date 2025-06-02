using LibraryApp.Domain.Books;
using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Infrastructure.Persistent.Configurations;

internal class BorrowRecordConfiguration : IEntityTypeConfiguration<BorrowRecord>
{
    public void Configure(EntityTypeBuilder<BorrowRecord> builder)
    {
        ConfigureBorrowRecordTable(builder);
    }

    private void ConfigureBorrowRecordTable(EntityTypeBuilder<BorrowRecord> builder)
    {
        builder.HasKey(e => e.Id).HasName("borrow_records_id_unique");

        builder.ToTable("borrow_records");

        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id")
            // .ValueGeneratedNever()
            .HasConversion(
              id => id.Value,
              value => BorrowRecordId.Create(value)
            );
        builder.Property(e => e.BookId)
            .HasColumnName("book_id")
            .HasConversion(
              id => id.Value,
              value => BookId.Create(value)
            );
        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasConversion(
              id => id.Value,
              value => UserId.Create(value)
            );
        builder.Property(e => e.BorrowDate)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp(3) without time zone")
            .HasColumnName("borrow_date");
        builder.Property(e => e.DueDate).HasColumnName("due_date");
        builder.Property(e => e.ReturnDate).HasColumnName("return_date");
        builder.Property(e => e.StatusValue)
            .HasColumnName("status");
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp(3) without time zone")
            .HasColumnName("created_at");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("timestamp(3) without time zone")
            .HasColumnName("updated_at");

        // builder.HasOne(d => d.Book).WithMany(p => p.BorrowRecords)
        //     .HasForeignKey(d => d.BookId)
        //     .HasConstraintName("borrow_records_book_id_books_id_fk");

        // builder.HasOne(d => d.User).WithMany(p => p.BorrowRecords)
        //     .HasForeignKey(d => d.UserId)
        //     .HasConstraintName("borrow_records_user_id_users_id_fk");
    }
}