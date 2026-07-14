using InventoryManager.Application.Warehouses;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class WarehousesController : ControllerBase
{
    private readonly IWarehouseService _WarehouseService;

    public WarehousesController(IWarehouseService warehouseService)
    {
        _WarehouseService = warehouseService;
    }

    [HttpGet]

    public async Task<ActionResult<IReadOnlyCollection<WarehouseResponse>>> GetWarehouses(
        CancellationToken cancellationToken)
    {
        var Warehouses = await _WarehouseService.GetWarehousesAsync(cancellationToken);

        return Ok(Warehouses);
    }

    [HttpGet("{id:int}")]

    public async Task<ActionResult<IReadOnlyCollection<WarehouseResponse>>> GetWarehousesById(
        int id,
        CancellationToken cancellationToken)
    {
        var Warehouse = await _WarehouseService.GetWarehouseByIdAsync(
            id,
            cancellationToken
        );

        if (Warehouse is null)
        {
            return NotFound();
        }

        return Ok(Warehouse);
    }

    [HttpPost]
    public async Task<ActionResult<WarehouseResponse>> CreateWarehouse(
        CreateWarehouseRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var Warehouse = await _WarehouseService.CreateWarehouseAsync(
                request,
                cancellationToken
            );

            return CreatedAtAction(
                nameof(GetWarehousesById),
                new {id = Warehouse.Id},
                Warehouse
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

    public async Task<IActionResult> DeleteWarehouse(
        int id,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var deleted = await _WarehouseService.DeleteWarehouseAsync(
                id,
                cancellationToken
            );

            if (!deleted) return NotFound();

            return NoContent();
        } catch (InvalidOperationException e)
        {
            return Conflict(e.Message);
        }
        
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult<WarehouseResponse>> UpdateWarehouse(
        int id,
        UpdateWarehouseRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var updatedWarehouse = await _WarehouseService.UpdateWarehouseAsync(
                id,
                request,
                cancellationToken
            );

            if (updatedWarehouse is null) return NotFound();

            return Ok(updatedWarehouse);
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