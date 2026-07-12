using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Domain.Categories;

namespace InventoryManager.Application.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IReadOnlyCollection<CategoryResponse>> GetCategoriesAsync(
        CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);

        return categories
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<CategoryResponse?> GetCategoryByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (category is null)
        {
            return null;
        }

        return MapToResponse(category);
    }

    public async Task<CategoryResponse> CreateCategoryAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var nameAlreadyExists = await _categoryRepository.NameExistsAsync(
            request.Name,
            cancellationToken);

        if (nameAlreadyExists)
        {
            throw new InvalidOperationException(
                $"A category with name '{request.Name}' already exists.");
        }

        var category = new Category(
            request.Name,
            request.Description);

        await _categoryRepository.AddAsync(category, cancellationToken);

        return MapToResponse(category);
    }

    public async Task<CategoryResponse?> UpdateCategoryAsync(
        int id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdForUpdateAsync(
            id,
            cancellationToken);

        if (category is null)
        {
            return null;
        }

        var nameAlreadyExists = await _categoryRepository.NameExistsForAnotherCategoryAsync(
            request.Name,
            id,
            cancellationToken);

        if (nameAlreadyExists)
        {
            throw new InvalidOperationException(
                $"A category with name '{request.Name}' already exists.");
        }

        category.Update(
            request.Name,
            request.Description);

        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return MapToResponse(category);
    }

    public async Task<bool> DeleteCategoryAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var hasProducts = await _categoryRepository.HasProductsAsync(
            id,
            cancellationToken);

        if (hasProducts)
        {
            throw new InvalidOperationException(
                "Cannot delete category because products are assigned to it.");
        }

        return await _categoryRepository.DeleteAsync(
            id,
            cancellationToken);
    }

    private static CategoryResponse MapToResponse(Category category)
    {
        return new CategoryResponse(
            category.Id,
            category.Name,
            category.Description);
    }
}