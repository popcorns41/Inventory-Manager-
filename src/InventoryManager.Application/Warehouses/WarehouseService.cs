using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Domain.Warehouses;

namespace InventoryManager.Application.Warehouses;

public class WarehouseService : IWarehouseService
{

    private readonly IWarehouseRepository _warehouseRepo;
    private readonly IWarehouseStockRepository _warehouseStockRepo;

    public WarehouseService(IWarehouseRepository warehouseRepository, IWarehouseStockRepository warehouseStockRepository)
    {
        _warehouseRepo = warehouseRepository;
        _warehouseStockRepo = warehouseStockRepository;
    }
    public Task<WarehouseResponse> CreateWarehouseAsync(CreateWarehouseRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteWarehouseAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<WarehouseResponse?> GetWarehouseByIdAsync(int id, CancellationToken cancellationToken)
    {   
        var warehouse = await _warehouseRepo.GetByIdAsync(id,cancellationToken);

        if (warehouse is null) return null;
        
        return MapToResponse(warehouse);
    }

    public async Task<IReadOnlyCollection<WarehouseResponse>> GetWarehousesAsync(CancellationToken cancellationToken)
    {
        var warehouses = await _warehouseRepo.GetAllAsync(cancellationToken);

        return warehouses
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<WarehouseResponse?> UpdateWarehouseAsync(int id, UpdateWarehouseRequest request, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepo.GetByIdForUpdateAsync(id,cancellationToken);

        if (warehouse is null)
        {
            return null;
        }

        var nameAlreadyExists = await _warehouseRepo.NameExistsForAnotherWarehouseAsync(
            request.Name,id,cancellationToken
        );

        if (nameAlreadyExists)
        {
            throw new InvalidOperationException(
                $"A warehouse with name '{request.Name}' already exists."
            );
        }

        var codeAlreadyExists = await _warehouseRepo.CodeExistsForAnotherWarehouseAsync(
            request.Code,id,cancellationToken
        );

        if (codeAlreadyExists)
        {
            throw new InvalidOperationException(
                $"A warehouse with code '{request.Code}' already exists."
            );
        }

        warehouse.Update(
            request.Code,
            request.Name,
            request.Description
        );

        await _warehouseRepo.SaveChangesAsync(cancellationToken);

        return MapToResponse(warehouse);
    }

    private WarehouseResponse MapToResponse(Warehouse warehouse)
    {
        return new WarehouseResponse(
                warehouse.Id,
                warehouse.Code,
                warehouse.Name,
                warehouse.Description
        );
    } 
}