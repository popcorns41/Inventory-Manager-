using System.Reflection;
using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Domain.Products;
using InventoryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace InventoryManager.Infrastructure.Products;

public class ProductRepository : IProductRepository
{
    private readonly InventoryDbContext _dbContext;

    public ProductRepository(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Product>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .OrderBy(product => product.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product =>product.Id == id, cancellationToken);
    }

    public async Task<bool> SkuExistsAsync(
        string sku,
        CancellationToken cancellationToken
    )
    {
        var normalisedSku = sku.Trim().ToLower();

        return await _dbContext.Products
            .AsNoTracking()
            .AnyAsync(
                product => product.Sku.ToLower() == normalisedSku,
                cancellationToken
            );
    }

    public async Task AddAsync(
        Product product,
        CancellationToken cancellationToken
    )
    {
        await _dbContext.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == id, cancellationToken);

        if (product is null) return false;

        _dbContext.Products.Remove(product);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }


}