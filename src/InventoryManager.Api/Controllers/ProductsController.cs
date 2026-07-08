using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    //Temporary DS of product responses
    //TODO: Replace with actual data source

    private static readonly List<ProductResponse> Products = new List<ProductResponse>
    {
        new ProductResponse(1, "Product 1", "Description for Product 1", 10.99m),
        new ProductResponse(2, "Product 2", "Description for Product 2", 19.99m),
        new ProductResponse(3, "Product 3", "Description for Product 3", 5.49m)
    };

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
}

public record ProductResponse(
    int Id,
    string Name,
    string Description,
    decimal Price
);