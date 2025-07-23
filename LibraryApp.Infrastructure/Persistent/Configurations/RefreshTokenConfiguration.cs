using LibraryApp.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Infrastructure.Persistent.Configurations;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        ConfigureRefreshTokenTable(builder);
    }

    private void ConfigureRefreshTokenTable(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.HasKey(e => e.Id)
            .HasName("refresh_tokens_id_unique");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Token)
            .HasColumnName("token")
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(e => e.Token).IsUnique();

        builder.Property(e => e.UserId)
            .HasColumnName("userId")
            .IsRequired()
            .HasMaxLength(256)
            .HasConversion(
              id => id.Value,
              value => UserId.Create(value)
            );
        
        builder.Property(e => e.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired()
            .HasColumnType("timestamp(3) without time zone");
        
        builder.Property(e => e.IsRevoked)
            .HasColumnName("is_revoked")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp(3) without time zone")
            .HasColumnName("created_at");
    }
}