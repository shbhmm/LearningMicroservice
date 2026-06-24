using CourseService.Data;
using CourseService.Interfaces;
using CourseService.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddSingleton<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseManager>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CourseService API",
        Version = "v1"
    });
});

var app = builder.Build();

// Seed data
var repo = app.Services.GetRequiredService<ICourseRepository>();
repo.Seed();

// Configure the HTTP request pipeline.
// Swagger is not required for the build environment

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CourseService API v1");
    });

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Handling request {Method} {Path}", context.Request.Method, context.Request.Path);
    await next();
    logger.LogInformation("Finished handling request.");
});

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));
app.MapControllers();

app.Run();
