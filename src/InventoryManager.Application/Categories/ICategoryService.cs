namespace InventoryManager.Application.Categories;

public interface ICategoryService
{
    Task<IReadOnlyCollection<CategoryResponse>> GetCategoriesAsync(
        CancellationToken cancellationToken);

    Task<CategoryResponse?> GetCategoryByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<CategoryResponse> CreateCategoryAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken);
    
    Task<bool> DeleteCategoryAsync(
        int id,
        CancellationToken cancellationToken
    );

    Task<CategoryResponse?> UpdateCategoryAsync(
        int id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken
    );
}