using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    //Temporary DS of product responses
    //TODO: Replace with actual data source

    private static readonly List<ProductResponse> Products =
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

    [HttpGet]
    public ActionResult<IEnumerable<ProductResponse>> GetProducts()
    {
        return Ok(Products);
    }

    [HttpGet("{id}")]
    public ActionResult<ProductResponse> GetProduct(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }


    //BIG NOTE: This appends to the in-memory list of products, hence volatile, will lose product when restarting the app.
    [HttpPost]
    public ActionResult<ProductResponse> CreateProduct(CreateProductRequest request)
    {
        var nextId = Products.Max(p => p.Id) + 1;

        var product = new ProductResponse(
            Id: nextId,
            Sku: request.Sku,
            Name: request.Name,
            Description: request.Description,
            Price: request.Price,
            QuantityInStock: request.QuantityInStock
        );

        Products.Add(product);

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public ActionResult<ProductResponse> UpdateProduct(int id, UpdateProductRequest request)
    {
        var productIndex = Products.FindIndex(p => p.Id == id);
        if (productIndex == -1)
        {
            return NotFound();
        }

        var updatedProduct = Products[productIndex] with
        {
            Sku = request.Sku,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            QuantityInStock = request.QuantityInStock
        };

        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteProduct(int id)
    {
        var productIndex = Products.FindIndex(p => p.Id == id);
        if (productIndex == -1)
        {
            return NotFound();
        }

        Products.RemoveAt(productIndex);
        return NoContent();
    }
}

public record CreateProductRequest(
    string Sku,
    string Name,
    string? Description,
    decimal Price,
    int QuantityInStock
);

public record UpdateProductRequest(
    string Sku,
    string Name,
    string? Description,
    decimal Price,
    int QuantityInStock
);

public record ProductResponse(
    int Id,
    string Sku,
    string Name,
    string? Description,
    decimal Price,
    int QuantityInStock
);