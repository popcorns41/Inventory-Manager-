namespace InventoryManager.Application.Products;

public record CreateProductRequest(
    string Sku,
    string Name,
    string? Description,
    decimal Price,
    int QuantityInStock
);