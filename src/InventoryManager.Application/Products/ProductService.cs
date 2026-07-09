using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Domain.Products;

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

    public async Task<ProductResponse> CreateProductAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var skuAlreadyExists = await _productRepository.SkuExistsAsync(
            request.Sku,
            cancellationToken);
        
        if(skuAlreadyExists)
        {
            throw new InvalidOperationException(
                $"A product with SKU '{request.Sku}' already exists."
            );
        }

        var product = new Product(
            request.Sku,
            request.Name,
            request.Description,
            request.Price,
            request.QuantityInStock
        );

        await _productRepository.AddAsync(product, cancellationToken);

        return MapToResponse(product);
    }

    public async Task<bool> DeleteProductAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        return await _productRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<ProductResponse?> UpdateProductAsync(
        int id,
        UpdateProductRequest request,
        CancellationToken cancellationToken
    )
    {
        var product = await _productRepository.getByIdForUpdateAsync(
            id,
            cancellationToken
        );

        if (product is null)
        {
            return null;
        }

        var skuAlreadyExists = await _productRepository.SkuExistsforAnotherProductAsync(
            request.Sku,
            id,
            cancellationToken
        );

        if (skuAlreadyExists)
        {
            throw new InvalidOperationException(
                $"A product with SKU '{request.Sku}' already exists."
            );
        }

        product.Update(
            request.Sku,
            request.Name,
            request.Description,
            request.Price,
            request.QuantityInStock
        );
        await _productRepository.SaveChangesAsync(cancellationToken);
        
        return MapToResponse(product);
    }

    private static ProductResponse MapToResponse(Product product)
    {
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