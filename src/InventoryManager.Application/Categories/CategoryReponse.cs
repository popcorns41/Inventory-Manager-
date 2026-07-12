namespace InventoryManager.Application.Categories;

public record CategoryResponse(
    int Id,
    string Name,
    string? Description
);