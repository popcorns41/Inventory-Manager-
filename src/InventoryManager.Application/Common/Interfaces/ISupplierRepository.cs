using System.Collections.Concurrent;
using InventoryManager.Domain.Suppliers;

namespace InventoryManager.Application.Common.Interfaces;

public interface ISupplierRepository
{
    Task<IReadOnlyCollection<Supplier>> GetAllAsync(
        CancellationToken cancellationToken
    );

    Task<Supplier?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken
    );

    Task<Supplier?> GetByIdForUpdateAsync(
        int id,
        CancellationToken cancellationToken
    );

    Task<bool> ExistsAsync(
        int id,
        CancellationToken cancellationToken
    );

    Task<bool> NameExistsAsync(
        string name,
        CancellationToken cancellationToken
    );

    Task<bool> NameExistsForAnotherSupplierAsync(
        string name,
        int supplierId,
        CancellationToken cancellationToken
    );

    Task<bool> HasProductsAsync(
        int supplierId,
        CancellationToken cancellationToken
    );

    Task AddAsync(
        Supplier supplier,
        CancellationToken cancellationToken
    );

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken
    );

    Task SaveChangesAsync(
        CancellationToken cancellationToken
    );


}