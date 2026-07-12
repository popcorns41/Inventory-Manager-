using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Domain.Categories;
using InventoryManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Infrastructure.Categories;

public class CategoryRepository : ICategoryRepository
{
    private readonly InventoryDbContext _dbContext;

    public CategoryRepository(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Category>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .OrderBy(category => category.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == id, cancellationToken);
    }

    public async Task<Category?> GetByIdForUpdateAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Categories
            .FirstOrDefaultAsync(category => category.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .AnyAsync(category => category.Id == id, cancellationToken);
    }

    public async Task<bool> NameExistsAsync(
        string name,
        CancellationToken cancellationToken)
    {
        var normalisedName = name.Trim().ToLower();

        return await _dbContext.Categories
            .AsNoTracking()
            .AnyAsync(
                category => category.Name.ToLower() == normalisedName,
                cancellationToken);
    }

    public async Task<bool> NameExistsForAnotherCategoryAsync(
        string name,
        int categoryId,
        CancellationToken cancellationToken)
    {
        var normalisedName = name.Trim().ToLower();

        return await _dbContext.Categories
            .AsNoTracking()
            .AnyAsync(
                category =>
                    category.Id != categoryId &&
                    category.Name.ToLower() == normalisedName,
                cancellationToken);
    }

    public async Task<bool> HasProductsAsync(
        int categoryId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .AnyAsync(
                product => product.CategoryId == categoryId,
                cancellationToken);
    }

    public async Task AddAsync(
        Category category,
        CancellationToken cancellationToken)
    {
        await _dbContext.Categories.AddAsync(category, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(category => category.Id == id, cancellationToken);

        if (category is null)
        {
            return false;
        }

        _dbContext.Categories.Remove(category);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}