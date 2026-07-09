using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Domain.Products;
using InventoryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
}