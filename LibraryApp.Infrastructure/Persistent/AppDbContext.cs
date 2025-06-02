using LibraryApp.Domain.Books;
using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.NameTranslation;

namespace DinnerApp.Infrastructure.Persistence;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowRecord> BorrowRecords { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum<BorrowStatusEnum>(null, "borrow_status", new NpgsqlNullNameTranslator())
            .HasPostgresEnum<RoleEnum>(null, "role", new NpgsqlNullNameTranslator())
            .HasPostgresEnum<StatusEnum>(null, "status", new NpgsqlNullNameTranslator());

        modelBuilder
          .ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}