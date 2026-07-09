using InventoryManager.Application.Common.Interfaces;

namespace InventoryManager.Application.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyCollection<ProductResponse>> GetProductsAsync(
        CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);

        return products
            .Select(product => new ProductResponse(
                product.Id,
                product.Sku,
                product.Name,
                product.Description,
                product.Price,
                product.QuantityInStock
            ))
            .ToList();
    }

    public async Task<ProductResponse?> GetProductByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);

        if (product is null)
        {
            return null;
        }

        return new ProductResponse(
            product.Id,
            product.Sku,
            product.Name,
            product.Description,
            product.Price,
            product.QuantityInStock
        );
    }
}