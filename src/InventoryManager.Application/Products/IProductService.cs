namespace InventoryManager.Application.Products;

public interface IProductService
{
    Task<IReadOnlyCollection<ProductResponse>> GetProductsAsync(
        CancellationToken cancellationToken);

    Task<ProductResponse?> GetProductByIdAsync(
        int id,
        CancellationToken cancellationToken);
}