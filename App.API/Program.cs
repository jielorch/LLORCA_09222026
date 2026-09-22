using App.Application;
using App.Infrastructure;
using App.Infrastructure.Security.Middlware;
using App.Infrastructure.Security.Services;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<ApiKeySecurityTransformer>();
});


builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("ENABLE_SWAGGER") == "true") // FOR TESTING PURPOSES ON DOCKER ACCESS THE SWAGGER UI
{
    app.MapOpenApi();
 
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1 API");
        options.RoutePrefix = "swagger";
    });
}

// Only enforce HTTPS redirection if NOT running inside a Docker container
if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != "true")
{
    app.UseHttpsRedirection();
}

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

// =========================================================
// AUTOMATIC CODE-FIRST MIGRATIONS ON STARTUP FOR DOCKER
// =========================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // 1. Get your DbContext instance (Replace with your actual class name)
        var context = services.GetRequiredService<AppDbContext>();

        // 2. Automatically execute 'dotnet ef database update' inside the container
        if (context.Database.IsRelational())
        {
            await context.Database.MigrateAsync();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while executing database migrations inside the container.");
    }
}

app.Run();


 