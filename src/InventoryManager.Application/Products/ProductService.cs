using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Domain.Products;

namespace InventoryManager.Application.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    private readonly IWarehouseStockRepository _warehouseStockRepository;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IWarehouseStockRepository warehouseStockRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _warehouseStockRepository = warehouseStockRepository;
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
                product.QuantityInStock,
                product.CategoryId,
                product.SupplierId
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
            product.QuantityInStock,
            product.CategoryId,
            product.SupplierId
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

        var categoryExists = await _categoryRepository.ExistsAsync(
            request.CategoryId,
            cancellationToken
        );

        if (!categoryExists)
        {
            throw new ArgumentException(
                $"Category with ID '{request.CategoryId}' does not exist.",
                nameof(request.CategoryId)
            );
        }

        var product = new Product(
            request.Sku,
            request.Name,
            request.Description,
            request.Price,
            request.QuantityInStock,
            request.CategoryId,
            request.SupplierId
        );

        await _productRepository.AddAsync(product, cancellationToken);

        return MapToResponse(product);
    }

    public async Task<bool> DeleteProductAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        var hasStockRecords = await _warehouseStockRepository.HasStockForProductAsync(
        id,
        cancellationToken);

        if (hasStockRecords)
        {
            throw new InvalidOperationException(
                $"Cannot delete product with ID '{id}' because it has warehouse stock records.");
        }

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

        var categoryExists = await _categoryRepository.ExistsAsync(
            request.CategoryId,
            cancellationToken);

        if (!categoryExists)
        {
            throw new ArgumentException(
                $"Category with ID '{request.CategoryId}' does not exist.",
                nameof(request.CategoryId));
        }

        product.Update(
            request.Sku,
            request.Name,
            request.Description,
            request.Price,
            request.QuantityInStock,
            request.CategoryId,
            request.SupplierId
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
            product.QuantityInStock,
            product.CategoryId,
            product.SupplierId
        );
    }
}