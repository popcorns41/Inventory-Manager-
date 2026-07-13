using InventoryManager.Domain.Warehouses;

namespace InventoryManager.Application.Common.Interfaces;

public interface IWarehouseStockRepository
{
    Task<WarehouseStock?> GetByProductAndWarehouseAsync(
        int productId,
        int warehouseId,
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(
        int productId,
        int warehouseId,
        CancellationToken cancellationToken);

    Task<bool> HasStockForWarehouseAsync(
        int warehouseId,
        CancellationToken cancellationToken);

    Task<bool> HasStockForProductAsync(
        int productId,
        CancellationToken cancellationToken);

    Task AddAsync(
        WarehouseStock warehouseStock,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        int productId,
        int warehouseId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}