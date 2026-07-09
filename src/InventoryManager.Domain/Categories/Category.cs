namespace InventoryManager.Domain.Categories;

public class Category
{
    private Category(){}

    public Category(string name, string? description)
    {
        ApplyDetails(name,description);
    }

    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public void Update(
        string name,
        string? description)
    {
        ApplyDetails(name, description);
    }

    private void ApplyDetails(string name, string? description)
    {
         if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name is required.", nameof(name));
        }

        Name = name.Trim();

        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();
    }
}