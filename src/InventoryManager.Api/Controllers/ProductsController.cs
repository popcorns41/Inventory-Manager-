using InventoryManager.Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

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
    public async Task<ActionResult<IReadOnlyCollection<ProductResponse>>> GetProducts(
        CancellationToken cancellationToken)
    {
        var products = await _productService.GetProductsAsync(cancellationToken);

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponse>> GetProductById(
        int id,
        CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct(
        CreateProductRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var product = await _productService.CreateProductAsync(
                request,
                cancellationToken);
            
            return CreatedAtAction(
                nameof(GetProductById),
                new {id = product.Id},
                product
            );
        }catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(exception.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var deleted = await _productService.DeleteProductAsync(
                id,
                cancellationToken);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(exception.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductResponse>> UpdateProduct(
        int id,
        UpdateProductRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var updatedProduct = await _productService.UpdateProductAsync(
                id,
                request,
                cancellationToken
            );

            if (updatedProduct is null) return NotFound();

            return Ok(updatedProduct);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(exception.Message);
        }
    }
}