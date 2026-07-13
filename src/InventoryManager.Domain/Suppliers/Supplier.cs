namespace InventoryManager.Domain.Suppliers;

public class Supplier
{
    private Supplier(){}

    public Supplier (string name, string? description)
    {
        ApplyDetails(name,description);
    }

    public int Id{get; private set; }

    public string Name{get; private set;} = string.Empty;

    public string? Description { get; private set; }

    public void Update(
        string name,
        string? description
    )
    {
        ApplyDetails(name, description);
    }

    private void ApplyDetails(string name, string? description)
    {
        if (string.IsNullOrEmpty(name))
        {
            //TODO: what is nameof?
            throw new ArgumentException("Supplier name is required.", nameof(name));
        }

        Name = name.Trim();

        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();
    }
}