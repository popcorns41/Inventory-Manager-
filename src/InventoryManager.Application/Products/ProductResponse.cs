namespace InventoryManager.Application.Products;

public record ProductResponse(
    int Id,
    string Sku,
    string Name,
    string? Description,
    decimal Price,
    int QuantityInStock,
    int categoryId
);