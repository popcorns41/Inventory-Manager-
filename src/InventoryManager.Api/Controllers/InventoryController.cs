using InventoryManager.Application.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class InventoryController : ControllerBase
{
    private IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpPut("warehouses/{warehouseId:int}/products/{productId:int}/stock")]
    public async Task<ActionResult<WarehouseStockReponse>> SetStock(
        int warehouseId,
        int productId,
        SetWarehouseStockRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var warehouseStock = await _inventoryService.SetStockAsync(
                productId,
                warehouseId,
                request,
                cancellationToken
            );

            return Ok(warehouseStock);
        } catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}   