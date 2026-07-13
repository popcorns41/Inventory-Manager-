namespace InventoryManager.Application.Suppliers;

public record SupplierResponse(
    int Id,
    string Name,
    string? Description
);