namespace InventoryManager.Application.Warehouses;

public record CreateWarehouseRequest(
    string Name,
    string Code,
    string? description
);