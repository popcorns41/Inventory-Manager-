namespace InventoryManager.Application.Warehouses;

public interface IWarehouseService
{
    Task<IReadOnlyCollection<WarehouseResponse>> GetWarehousesAsync(
        CancellationToken cancellationToken);

    Task<WarehouseResponse?> GetWarehouseByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<WarehouseResponse> CreateWarehouseAsync(
        CreateWarehouseRequest request,
        CancellationToken cancellationToken);
    
    Task<bool> DeleteWarehouseAsync(
        int id,
        CancellationToken cancellationToken
    );

    Task<WarehouseResponse?> UpdateWarehouseAsync(
        int id,
        UpdateWarehouseRequest request,
        CancellationToken cancellationToken
    );
}