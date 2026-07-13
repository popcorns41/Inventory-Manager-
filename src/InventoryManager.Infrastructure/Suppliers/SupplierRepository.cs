using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Domain.Suppliers;
using InventoryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Infrastructure.Suppliers;

public class SupplierRepository : ISupplierRepository
{
    private readonly InventoryDbContext _dbContext;

    public SupplierRepository(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Supplier>> GetAllAsync(
        CancellationToken cancellationToken
    )
    {
        return await _dbContext.Suppliers
            .AsNoTracking()
            .OrderBy(supplier => supplier.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Supplier?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        return await _dbContext.Suppliers
            .AsNoTracking()
            .FirstOrDefaultAsync(supplier => supplier.Id == id, cancellationToken);
    }

    public async Task<Supplier?> GetByIdForUpdateAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        return await _dbContext.Suppliers
            .FirstOrDefaultAsync(supplier => supplier.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        return await _dbContext.Suppliers
            .AsNoTracking()
            .AnyAsync(supplier => supplier.Id == id, cancellationToken);
    }

    public async Task<bool> NameExistsAsync(
        string name,
        CancellationToken cancellationToken
    )
    {
        var normalisedName = name.Trim().ToLower();

        return await _dbContext.Suppliers
            .AsNoTracking()
            .AnyAsync(
                supplier => supplier.Name.ToLower() == normalisedName,
                cancellationToken
            );
    }

    public async Task<bool> NameExistsForAnotherSupplierAsync(
        string name,
        int categoryId,
        CancellationToken cancellationToken
    )
    {
        var normalisedName = name.Trim().ToLower();

        return await _dbContext.Suppliers
            .AsNoTracking()
            .AnyAsync(
                category =>
                    category.Id != categoryId &&
                    category.Name.ToLower() == normalisedName,
                    cancellationToken);
    }

    public async Task<bool> HasProductsAsync(
        int supplierId,
        CancellationToken cancellationToken
    )
    {
        return await _dbContext.Products
            .AsNoTracking()
            .AnyAsync(
                product => product.SupplierId == supplierId,
                cancellationToken);
    }

    public async Task AddAsync(
        Supplier supplier,
        CancellationToken cancellationToken
    )
    {
        await _dbContext.Suppliers.AddAsync(supplier,cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken); 
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        var supplier = await _dbContext.Suppliers
            .FirstOrDefaultAsync(supplier => supplier.Id == id, cancellationToken);
        
        if (supplier is null)
        {
            return false;
        }

        _dbContext.Suppliers.Remove(supplier);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}