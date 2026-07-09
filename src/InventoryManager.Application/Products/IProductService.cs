namespace InventoryManager.Application.Products;

public interface IProductService
{
    Task<IReadOnlyCollection<ProductResponse>> GetProductsAsync(
        CancellationToken cancellationToken);

    Task<ProductResponse?> GetProductByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<ProductResponse> CreateProductAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken);
    
    Task<bool> DeleteProductAsync(
        int id,
        CancellationToken cancellationToken
    );
}