// using LibraryApp.Api.Common.Errors;
using LibraryApp.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LibraryApp.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });
        
        services.AddMappings();
        services.AddOpenApi();
        // services.AddSingleton<ProblemDetailsFactory, AppProblemDetailFactory>();
        services.AddProblemDetails();
        return services;
    }
}