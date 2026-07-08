using InventoryManager.Application.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Temporary DS of product responses, will change to a database or other data source in the future
//TODO: Replace with actual data source

builder.Services.AddSingleton<IProductService, ProductService>();

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