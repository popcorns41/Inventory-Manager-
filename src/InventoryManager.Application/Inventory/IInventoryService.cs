namespace InventoryManager.Application.Inventory;

public interface IInventoryService
{
    Task<WarehouseStockReponse> SetStockAsync(
        int productId,
        int warehouseId,
        SetWarehouseStockRequest request,
        CancellationToken cancellationToken
    );
}