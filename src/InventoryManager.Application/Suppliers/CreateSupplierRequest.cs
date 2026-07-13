namespace InventoryManager.Application.Suppliers;

public record CreateSupplierRequest(
    string Name,
    string? Description
);