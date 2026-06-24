using CourseService.Data;
using CourseService.Interfaces;
using CourseService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddSingleton<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseManager>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed data
var repo = app.Services.GetRequiredService<ICourseRepository>();
repo.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
