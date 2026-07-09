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
        int quantityInStock)
    {
        //safe validation to prevent formatting misalign in our DB

        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new ArgumentException("SKU is required.", nameof(sku));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.",nameof(name));
        }

        if (price < 0)
        {
            throw new ArgumentException("Price cannot be negative.", nameof(price));
        }

        if (QuantityInStock < 0)
        {
            throw new ArgumentException("Quantity in stock cannot be negative.", nameof(quantityInStock));
        }

        Sku = sku.Trim();
        Name = name.Trim();
        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();
        Price = price;
        QuantityInStock = quantityInStock;
    }
    public int Id { get; set; }

    public string Sku { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int QuantityInStock { get; set; }
}