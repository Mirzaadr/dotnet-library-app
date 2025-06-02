using LibraryApp.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Infrastructure.Persistent.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUserTable(builder);
    }

    private void ConfigureUserTable(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id).HasName("users_id_unique");

        builder.ToTable("users");

        builder.HasIndex(e => e.Email, "users_email_key").IsUnique();

        builder.HasIndex(e => e.UniversityId, "users_university_id_unique").IsUnique();

        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id")
            .ValueGeneratedNever()
            .HasConversion(
              id => id.Value,
              value => UserId.Create(value)
            );
        builder.Property(e => e.Email).HasColumnName("email");
        builder.Property(e => e.FullName).HasColumnName("full_name");
        builder.Property(e => e.LastActivityDate)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp(3) without time zone")
            .HasColumnName("last_activity_date");
        builder.Property(e => e.Password).HasColumnName("password");
        builder.Property(e => e.UniversityCard).HasColumnName("university_card");
        builder.Property(e => e.UniversityId).HasColumnName("university_id");

        builder.Property(e => e.RoleValue)
            .HasColumnName("role");

        builder.Property(e => e.StatusValue)
            .HasColumnName("status");

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp(3) without time zone")
            .HasColumnName("created_at");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("timestamp(3) without time zone")
            .HasColumnName("updated_at");
    }
}