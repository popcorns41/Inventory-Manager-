namespace InventoryManager.Application.Products;

public interface IProductService
{
    IReadOnlyCollection<ProductResponse> GetProducts();

    ProductResponse? GetProductById(int id);

    ProductResponse CreateProduct(CreateProductRequest request);

    ProductResponse? UpdateProduct(int id, UpdateProductRequest request);

    bool DeleteProduct(int id);
}