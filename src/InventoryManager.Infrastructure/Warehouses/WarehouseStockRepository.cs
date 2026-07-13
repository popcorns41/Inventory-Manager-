using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Domain.Warehouses;
using InventoryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Infrastructure.Warehouses;

public class WarehouseStockRepository : IWarehouseStockRepository
{
    private readonly InventoryDbContext _dbContext;

    public WarehouseStockRepository(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        WarehouseStock warehouseStock,
        CancellationToken cancellationToken)
    {
        await _dbContext.WarehouseStocks.AddAsync(
            warehouseStock,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        int productId,
        int warehouseId,
        CancellationToken cancellationToken)
    {
        var warehouseStock = await _dbContext.WarehouseStocks
            .FirstOrDefaultAsync(
                stock =>
                    stock.ProductId == productId &&
                    stock.WarehouseId == warehouseId,
                cancellationToken);

        if (warehouseStock is null)
        {
            return false;
        }

        _dbContext.WarehouseStocks.Remove(warehouseStock);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ExistsAsync(
        int productId,
        int warehouseId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.WarehouseStocks
            .AsNoTracking()
            .AnyAsync(
                stock =>
                    stock.ProductId == productId &&
                    stock.WarehouseId == warehouseId,
                cancellationToken);
    }

    public async Task<WarehouseStock?> GetByProductAndWarehouseAsync(
        int productId,
        int warehouseId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.WarehouseStocks
            .FirstOrDefaultAsync(
                stock =>
                    stock.ProductId == productId &&
                    stock.WarehouseId == warehouseId,
                cancellationToken);
    }

    public async Task<bool> HasStockForProductAsync(
        int productId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.WarehouseStocks
            .AsNoTracking()
            .AnyAsync(
                stock => stock.ProductId == productId,
                cancellationToken);
    }

    public async Task<bool> HasStockForWarehouseAsync(
        int warehouseId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.WarehouseStocks
            .AsNoTracking()
            .AnyAsync(
                stock => stock.WarehouseId == warehouseId,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}