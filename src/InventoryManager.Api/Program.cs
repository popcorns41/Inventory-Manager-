using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Application.Products;
using InventoryManager.Infrastructure.Persistence;
using InventoryManager.Infrastructure.Products;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<InventoryDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Inventory Manager API v1");
    });
}

app.MapGet("/health", () => new
{
    status = "Healthy",
    service = "InventoryManager.Api",
    timestamp = DateTime.UtcNow
});

app.MapControllers();

app.Run();