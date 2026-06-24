using StudentService.Data;
using StudentService.Interfaces;
using StudentService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentManager>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var repo = app.Services.GetRequiredService<IStudentRepository>();
repo.Seed();

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
