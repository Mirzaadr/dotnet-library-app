// using LibraryApp.Api.Common.Errors;
using LibraryApp.Api.Common.Errors;
using LibraryApp.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LibraryApp.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddMappings();
        services.AddOpenApi();
        return services;
    }
}