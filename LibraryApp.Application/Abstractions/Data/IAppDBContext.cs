using LibraryApp.Domain.Books;
using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

public interface IAppDBContext
{
    DbSet<User> Users { get; }
    DbSet<Book> Books { get; }
    DbSet<BorrowRecord> BorrowRecords { get; }
    DbSet<RefreshToken> RefreshTokens { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}