using LibraryApp.Infrastructure;
using LibraryApp.Application;
using LibraryApp.Api;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddPresentation();

builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("AppDb")!);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowedOrigins",
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler("/error");
app.UseStatusCodePages();
app.UseHttpsRedirection();

app.UseCors("allowedOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapControllers();
app.Run();
