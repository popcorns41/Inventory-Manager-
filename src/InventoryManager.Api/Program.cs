var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/health", () => new
{
    status = "Healthy",
    service = "InventoryManager.Api",
    timestamp = DateTime.UtcNow
});

app.Run();