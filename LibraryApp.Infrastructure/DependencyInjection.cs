using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DinnerApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Domain.Users;
using Npgsql.NameTranslation;
using LibraryApp.Domain.BorrowRecords;
using LibraryApp.Application.Abstractions.Authentication;
using LibraryApp.Infrastructure.Authentication;
using LibraryApp.Application.Abstractions.Services;
using LibraryApp.Infrastructure.Services;

using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddAuth(configuration)
            .AddPersistent(configuration);

        return services;
    }

    internal static IServiceCollection AddPersistent(
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
    internal static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddScoped<IAppDBContext, AppDbContext>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IPasswordHasher, BCryptHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        services.AddAuthorization();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecretKey)
                    )
                };
            });
        return services;
    }
}

