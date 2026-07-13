using InventoryManager.Domain.Warehouses;

namespace InventoryManager.Application.Common.Interfaces;

public interface IWarehouseRepository
{
    Task<IReadOnlyCollection<Warehouse>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Warehouse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<Warehouse?> GetByIdForUpdateAsync(
        int id,
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(
        int id,
        CancellationToken cancellationToken);

    Task<bool> NameExistsAsync(
        string name,
        CancellationToken cancellationToken);

    Task<bool> NameExistsForAnotherWarehouseAsync(
        string name,
        int warehouseId,
        CancellationToken cancellationToken);

    Task<bool> HasStockForWarehouseAsync(
        int WarehouseId,
        CancellationToken cancellationToken);

    Task AddAsync(
        Warehouse warehouse,
        CancellationToken cancellationToken);

    //Ensure logic in API is in place to make enforce that no products exist
    //within a Warehouse before deletion.

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken);
    
    Task<bool> CodeExistsAsync( 
        string code,
        CancellationToken cancellationToken);
    
    Task<bool> CodeExistsForAnotherWarehouseAsync(
        string code,
        int warehouseId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}