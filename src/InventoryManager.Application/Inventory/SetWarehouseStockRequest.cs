namespace InventoryManager.Application.Inventory;

public record SetWarehouseStockRequest(
    int QuantityOnHand
);