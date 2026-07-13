using InventoryManager.Domain.Suppliers;

namespace InventoryManager.Domain.Products;

public class Product
{   
    //Needed by EF core.
    private Product(){}

    public Product(
        string sku,
        string name,
        string? description,
        decimal price,
        int quantityInStock,
        int categoryId,
        int supplierId)
    {
        //safe validation to prevent formatting misalign in our DB
        ApplyDetails(
            sku,
            name,
            description,
            price,
            quantityInStock,
            categoryId,
            supplierId
        );
    }

    public void Update(
        string Sku,
        string name,
        string? description,
        decimal price,
        int quantityInStock,
        int categoryId,
        int supplierId
    )
    {
        ApplyDetails(
            Sku,
            name,
            description,
            price,
            quantityInStock,
            categoryId,
            supplierId
        );
    }
    public int Id { get; set; }

    public string Sku { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int QuantityInStock { get; set; }

    public int CategoryId {get; set; }

    public int SupplierId {get; set; }

    //Helper to both validate product parameters and then re-define class attributes
    private void ApplyDetails(
        string sku,
        string name,
        string? description,
        decimal price,
        int quantityInStock,
        int categoryId,
        int supplierId)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new ArgumentException("SKU is required.", nameof(sku));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        if (price < 0)
        {
            throw new ArgumentException("Price cannot be negative.", nameof(price));
        }

        if (quantityInStock < 0)
        {
            throw new ArgumentException(
                "Quantity in stock cannot be negative.",
                nameof(quantityInStock));
        }

        Sku = sku.Trim();
        Name = name.Trim();
        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();
        Price = price;
        QuantityInStock = quantityInStock;
        CategoryId = categoryId;
        SupplierId = supplierId;
    }
}