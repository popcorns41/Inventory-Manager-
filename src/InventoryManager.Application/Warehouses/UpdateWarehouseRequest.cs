namespace InventoryManager.Application.Warehouses;

public record UpdateWarehouseRequest(
    string Name,
    string Code,
    string? Description
);