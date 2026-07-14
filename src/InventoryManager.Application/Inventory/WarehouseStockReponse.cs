namespace InventoryManager.Application.Inventory;

public record WarehouseStockReponse(
    int ProductId,
    int WarehouseId,
    int QuantityOnHand
);