using EnrollmentService.Data;
using EnrollmentService.Interfaces;
using EnrollmentService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSingleton<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentManager>();

// HttpClient to call other services. Use service names from docker-compose
builder.Services.AddHttpClient("courses", c => c.BaseAddress = new Uri(builder.Configuration["CourseServiceUrl"] ?? "http://courseservice"));
builder.Services.AddHttpClient("students", c => c.BaseAddress = new Uri(builder.Configuration["StudentServiceUrl"] ?? "http://studentservice"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var repo = app.Services.GetRequiredService<IEnrollmentRepository>();
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
