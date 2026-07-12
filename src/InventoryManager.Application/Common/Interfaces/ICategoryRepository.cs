using InventoryManager.Domain.Categories;

namespace InventoryManager.Application.Common.Interfaces;

public interface ICategoryRepository
{
    Task<IReadOnlyCollection<Category>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Category?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<Category?> GetByIdForUpdateAsync(
        int id,
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(
        int id,
        CancellationToken cancellationToken);

    Task<bool> NameExistsAsync(
        string name,
        CancellationToken cancellationToken);

    Task<bool> NameExistsForAnotherCategoryAsync(
        string name,
        int categoryId,
        CancellationToken cancellationToken);

    //Necessary for deletion of a Category,
    //Check no products are of a category to delete
    Task<bool> HasProductsAsync(
        int categoryId,
        CancellationToken cancellationToken);

    Task AddAsync(
        Category category,
        CancellationToken cancellationToken);

    //Ensure logic in API is in place to make enforce that no products exist
    //within a category before deletion.

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}