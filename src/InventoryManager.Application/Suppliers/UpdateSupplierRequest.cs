namespace InventoryManager.Application.Suppliers;

public record UpdateSupplierRequest(
    string Name,
    string? Description
);