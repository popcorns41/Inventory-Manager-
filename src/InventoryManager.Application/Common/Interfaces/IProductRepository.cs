using InventoryManager.Domain.Products;

namespace InventoryManager.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyCollection<Product>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<bool> SkuExistsAsync(
        string sku,
        CancellationToken cancellationToken);

    Task AddAsync(
        Product product,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken
    );

    Task<Product?> getByIdForUpdateAsync(
        int id,
        CancellationToken cancellationToken);
    
    Task<bool> SkuExistsforAnotherProductAsync(
        string sku,
        int productId,
        CancellationToken cancellationToken
    );

    Task SaveChangesAsync(
        CancellationToken cancellationToken
    );
}
