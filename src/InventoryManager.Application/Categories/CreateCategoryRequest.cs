namespace InventoryManager.Application.Categories;

public record CreateCategoryRequest(
    string Name,
    string? Description
);