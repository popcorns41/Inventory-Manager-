namespace InventoryManager.Application.Warehouses;

public record WarehouseResponse(
    int Id,
    string Code,
    string Name,
    string? Description
);