using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Domain.Warehouses;
using InventoryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Infrastructure.Warehouses;
public class WarehouseRepository : IWarehouseRepository
{

    private readonly InventoryDbContext _dbContext;

    public WarehouseRepository(InventoryDbContext inventoryDbContext)
    {
        _dbContext = inventoryDbContext;
    }
    public async Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        await _dbContext.Warehouses.AddAsync(warehouse,cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CodeExistsAsync(string code, CancellationToken cancellationToken)
    {
        var normalisedCode = code.Trim().ToUpper();

        return await _dbContext.Warehouses
            .AsNoTracking()
            .AnyAsync(
                warehouse => warehouse.Code == normalisedCode,
                cancellationToken
            );
    }

    public async Task<bool> CodeExistsForAnotherWarehouseAsync(string code, int warehouseId, CancellationToken cancellationToken)
    {
        var normalisedCode = code.Trim().ToUpper();

        return await _dbContext.Warehouses
            .AsNoTracking()
            .AnyAsync(
                warehouse => warehouse.Code == normalisedCode &&
                warehouse.Id != warehouseId,
                cancellationToken
            );
    }

    public async Task<bool> DeleteAsync(
    int id,
    CancellationToken cancellationToken)
    {
        var warehouse = await _dbContext.Warehouses
            .FirstOrDefaultAsync(
                warehouse => warehouse.Id == id,
                cancellationToken);

        if (warehouse is null)
        {
            return false;
        }

        _dbContext.Warehouses.Remove(warehouse);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ExistsAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Warehouses
            .AsNoTracking()
            .AnyAsync(
                warehouse => warehouse.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyCollection<Warehouse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Warehouses
            .AsNoTracking()
            .OrderBy(warehouse => warehouse.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Warehouse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Warehouses
            .AsNoTracking()
            .FirstOrDefaultAsync(warehouse => warehouse.Id == id, cancellationToken);
    }

    public async Task<Warehouse?> GetByIdForUpdateAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Warehouses
            .FirstOrDefaultAsync(warehouse => warehouse.Id == id, cancellationToken);
    }


    //To block deletion if stock exists
    public async Task<bool> HasStockForWarehouseAsync(int WarehouseId, CancellationToken cancellationToken)
    {
        return await _dbContext.WarehouseStocks
        .AsNoTracking()
        .AnyAsync(stock => stock.WarehouseId == WarehouseId, cancellationToken
        );
    }

    public async Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken)
    {
        var normalisedName = name.Trim().ToLower();
        return await _dbContext.Warehouses
            .AsNoTracking()
            .AnyAsync(
                warehouse => warehouse.Name.ToLower() == normalisedName,
                cancellationToken);
    }

    public async Task<bool> NameExistsForAnotherWarehouseAsync(
        string name,
        int warehouseId,
        CancellationToken cancellationToken)
    {
        var normalisedName = name.Trim().ToLower();

        return await _dbContext.Warehouses
            .AsNoTracking()
            .AnyAsync(
                warehouse =>
                    warehouse.Id != warehouseId &&
                    warehouse.Name.ToLower() == normalisedName,
                cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}