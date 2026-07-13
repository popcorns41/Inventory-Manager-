using InventoryManager.Application.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.Api.controllers;

[ApiController]
[Route("api/[controller]")]

public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _SupplierService;

    public SuppliersController(ISupplierService SupplierService)
    {
        _SupplierService = SupplierService;
    }

    [HttpGet]

    public async Task<ActionResult<IReadOnlyCollection<SupplierResponse>>> GetSuppliers(
        CancellationToken cancellationToken)
    {
        var Suppliers = await _SupplierService.GetSuppliersAsync(cancellationToken);

        return Ok(Suppliers);
    }

    [HttpGet("{id:int}")]

    public async Task<ActionResult<IReadOnlyCollection<SupplierResponse>>> GetSuppliersById(
        int id,
        CancellationToken cancellationToken)
    {
        var Supplier = await _SupplierService.GetSupplierByIdAsync(
            id,
            cancellationToken
        );

        if (Supplier is null)
        {
            return NotFound();
        }

        return Ok(Supplier);
    }

    [HttpPost]
    public async Task<ActionResult<SupplierResponse>> CreateSupplier(
        CreateSupplierRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var Supplier = await _SupplierService.CreateSupplierAsync(
                request,
                cancellationToken
            );

            return CreatedAtAction(
                nameof(GetSuppliersById),
                new {id = Supplier.Id},
                Supplier
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

    public async Task<IActionResult> DeleteSupplier(
        int id,
        CancellationToken cancellationToken
    )
    {   

        var deleted = await _SupplierService.DeleteSupplierAsync(
            id,
            cancellationToken
        );

        if (!deleted) return NotFound();

        return NoContent();
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult<SupplierResponse>> UpdateSupplier(
        int id,
        UpdateSupplierRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var updatedSupplier = await _SupplierService.UpdateSupplierAsync(
                id,
                request,
                cancellationToken
            );

            if (updatedSupplier is null) return NotFound();

            return Ok(updatedSupplier);
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