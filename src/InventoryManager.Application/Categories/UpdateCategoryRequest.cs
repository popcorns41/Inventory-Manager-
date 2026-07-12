namespace InventoryManager.Application.Categories;

public record UpdateCategoryRequest(
    string Name,
    string? Description
);