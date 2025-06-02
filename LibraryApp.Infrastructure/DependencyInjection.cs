using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DinnerApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Domain.Users;
using Npgsql.NameTranslation;
using LibraryApp.Domain.BorrowRecords;

namespace LibraryApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("AppDb"),
            npgsqlOptions =>
            {
                npgsqlOptions.MapEnum<RoleEnum>("role", null, new NpgsqlNullNameTranslator());
                npgsqlOptions.MapEnum<StatusEnum>("status", null, new NpgsqlNullNameTranslator());
                npgsqlOptions.MapEnum<BorrowStatusEnum>("borrow_status", null, new NpgsqlNullNameTranslator());
            })
        );
        return services;
    }
}

