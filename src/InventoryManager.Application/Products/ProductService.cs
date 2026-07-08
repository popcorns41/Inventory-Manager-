namespace InventoryManager.Application.Products;
    
public class ProductService : IProductService
{
    private readonly List<ProductResponse> _products =
    [
        new ProductResponse(
            Id: 1,
            Sku: "SKU-001",
            Name: "Mechanical Keyboard",
            Description: "Compact mechanical keyboard",
            Price: 79.99m,
            QuantityInStock: 25
        ),
        new ProductResponse(
            Id: 2,
            Sku: "SKU-002",
            Name: "USB-C Cable",
            Description: "1m USB-C charging cable",
            Price: 9.99m,
            QuantityInStock: 100
        )
    ];

    public IReadOnlyCollection<ProductResponse> GetProducts()
    {
        return _products.AsReadOnly();
    }

    public ProductResponse? GetProductById(int id)
    {
        return _products.FirstOrDefault(product => product.Id == id);
    }

    public ProductResponse CreateProduct(CreateProductRequest request)
    {
        var nextId = _products.Count == 0
            ? 1
            : _products.Max(product => product.Id) + 1;

        var product = new ProductResponse(
            Id: nextId,
            Sku: request.Sku,
            Name: request.Name,
            Description: request.Description,
            Price: request.Price,
            QuantityInStock: request.QuantityInStock
        );

        _products.Add(product);

        return product;
    }

    public ProductResponse? UpdateProduct(int id, UpdateProductRequest request)
    {
        var productIndex = _products.FindIndex(product => product.Id == id);

        if (productIndex == -1)
        {
            return null;
        }

        var updatedProduct = _products[productIndex] with
        {
            Sku = request.Sku,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            QuantityInStock = request.QuantityInStock
        };

        _products[productIndex] = updatedProduct;

        return updatedProduct;
    }

    public bool DeleteProduct(int id)
    {
        var productIndex = _products.FindIndex(product => product.Id == id);

        if (productIndex == -1)
        {
            return false;
        }

        _products.RemoveAt(productIndex);

        return true;
    }
}