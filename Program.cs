var builder = WebApplication.CreateBuilder(args);

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Test endpoint
app.MapGet("/", () => "WorkoutTrackerApi is running");

app.MapGet("/weatherforecast", () =>
{
    return new[]
    {
        new { Date = DateTime.Now, TemperatureC = 20, Summary = "Warm" }
    };
});

app.Run();