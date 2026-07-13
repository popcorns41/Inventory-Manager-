using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Domain.Suppliers;

namespace InventoryManager.Application.Suppliers;

public class SupplierService : ISupplierService
{

    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }
    public async Task<IReadOnlyCollection<SupplierResponse>> GetSuppliersAsync(
        CancellationToken cancellationToken)
    {
        var suppliers = await _supplierRepository.GetAllAsync(cancellationToken);

        return suppliers
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<SupplierResponse?> GetSupplierByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id,cancellationToken);

        if (supplier is null) return null;

        return MapToResponse(supplier);
    }

    public async Task<SupplierResponse> CreateSupplierAsync(
        CreateSupplierRequest request,
        CancellationToken cancellationToken)
    {
        var nameAlreadyExists = await _supplierRepository.NameExistsAsync(
            request.Name, 
            cancellationToken);
        
        if (nameAlreadyExists)
        {
            throw new InvalidOperationException(
                $"A Supplier with name '{request.Name}' already exists.");
        }

        var supplier = new Supplier(
            request.Name,
            request.Description
        );

        await _supplierRepository.AddAsync(supplier, cancellationToken);

        return MapToResponse(supplier);
    }
    
    public async Task<bool> DeleteSupplierAsync(
        int id,
        CancellationToken cancellationToken
    )
    {
        var hasProducts = await _supplierRepository.HasProductsAsync(
            id,
            cancellationToken
        );

        if (hasProducts)
        {
            throw new InvalidOperationException(
                "Cannot delete supplier because products are assigned to it.");
        }

        return await _supplierRepository.DeleteAsync(
            id,
            cancellationToken
        );
    }

    public async Task<SupplierResponse?> UpdateSupplierAsync(
        int id,
        UpdateSupplierRequest request,
        CancellationToken cancellationToken
    )
    {
        var supplier = await _supplierRepository.GetByIdForUpdateAsync(
            id,
            cancellationToken
        );

        if (supplier is null)
        {
            return null;
        }

        var nameAlreadyExists = await _supplierRepository.NameExistsForAnotherSupplierAsync(
            request.Name,
            id,
            cancellationToken);
        
        if (nameAlreadyExists)
        {
            throw new InvalidOperationException(
                $"A supplier with name '{request.Name}' already exists.");
        }

        supplier.Update(
            request.Name,
            request.Description);

        await _supplierRepository.SaveChangesAsync(cancellationToken);

        return MapToResponse(supplier);
    }

    private static SupplierResponse MapToResponse(Supplier supplier)
    {
        return new SupplierResponse(
            supplier.Id,
            supplier.Name,
            supplier.Description);
    }
}