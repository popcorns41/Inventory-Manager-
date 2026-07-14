using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Application.Products;
using InventoryManager.Infrastructure.Products;

using InventoryManager.Application.Categories;
using InventoryManager.Infrastructure.Categories;

using InventoryManager.Application.Suppliers;
using InventoryManager.Infrastructure.Suppliers;

using InventoryManager.Application.Warehouses;
using InventoryManager.Infrastructure.Warehouses;

using InventoryManager.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using InventoryManager.Application.Inventory;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IWarehouseStockRepository, WarehouseStockRepository>();

builder.Services.AddScoped<IInventoryService, InventoryService>(); 

builder.Services.AddDbContext<InventoryDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

builder.Services.AddOpenApi();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

app.UseCors("Frontend");

app.MapControllers();

app.Run();