using InventoryManager.Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

//TODO: Validation!

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductResponse>> GetProducts()
    {
        var products = _productService.GetProducts();

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public ActionResult<ProductResponse> GetProductById(int id)
    {
        var product = _productService.GetProductById(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<ProductResponse> CreateProduct(CreateProductRequest request)
    {
        var product = _productService.CreateProduct(request);

        return CreatedAtAction(
            nameof(GetProductById),
            new { id = product.Id },
            product
        );
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProductResponse> UpdateProduct(int id, UpdateProductRequest request)
    {
        var updatedProduct = _productService.UpdateProduct(id, request);

        if (updatedProduct is null)
        {
            return NotFound();
        }

        return Ok(updatedProduct);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteProduct(int id)
    {
        var deleted = _productService.DeleteProduct(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}