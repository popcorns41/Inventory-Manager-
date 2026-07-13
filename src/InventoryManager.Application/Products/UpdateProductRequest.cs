namespace InventoryManager.Application.Products;

public record UpdateProductRequest(
    string Sku,
    string Name,
    string? Description,
    decimal Price,
    int QuantityInStock,
    int CategoryId,
    int SupplierId
);
