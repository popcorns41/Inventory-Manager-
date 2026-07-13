namespace InventoryManager.Application.Suppliers;

public interface ISupplierService
{
    Task<IReadOnlyCollection<SupplierResponse>> GetSuppliersAsync(
        CancellationToken cancellationToken);

    Task<SupplierResponse?> GetSupplierByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<SupplierResponse> CreateSupplierAsync(
        CreateSupplierRequest request,
        CancellationToken cancellationToken);
    
    Task<bool> DeleteSupplierAsync(
        int id,
        CancellationToken cancellationToken
    );

    Task<SupplierResponse?> UpdateSupplierAsync(
        int id,
        UpdateSupplierRequest request,
        CancellationToken cancellationToken
    );
}