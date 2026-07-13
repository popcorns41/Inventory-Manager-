namespace InventoryManager.Domain.Warehouses;

public class Warehouse
{
    private Warehouse()
    {
        // Required by EF Core.
    }

    public Warehouse(
        string code,
        string name,
        string? description)
    {
        ApplyDetails(code, name, description);
    }

    public int Id { get; private set; }

    public string Code { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public void Update(
        string code,
        string name,
        string? description)
    {
        ApplyDetails(code, name, description);
    }

    private void ApplyDetails(
        string code,
        string name,
        string? description)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException(
                "Warehouse code is required.",
                nameof(code));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Warehouse name is required.",
                nameof(name));
        }

        Code = code.Trim().ToUpperInvariant();
        Name = name.Trim();

        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();
    }
}