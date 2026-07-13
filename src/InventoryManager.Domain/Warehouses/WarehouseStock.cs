namespace InventoryManager.Domain.Warehouses;

public class WarehouseStock
{
    private WarehouseStock(){}

    public WarehouseStock(
        int productId,
        int warehouseId,
        int quantityOnHand)
    {
        if (productId <= 0)
        {
            throw new ArgumentException(
                "Product ID must be a positive number.",
                nameof(productId));
        }

        if (warehouseId <= 0)
        {
            throw new ArgumentException(
                "Warehouse ID must be a positive number.",
                nameof(warehouseId));
        }

        ProductId = productId;
        WarehouseId = warehouseId;

        SetQuantity(quantityOnHand);
    }

    public int ProductId { get; private set; }

    public int WarehouseId { get; private set; }

    public int QuantityOnHand { get; private set; }

    public void SetQuantity(int quantityOnHand)
    {
        if (quantityOnHand < 0)
        {
            throw new ArgumentException(
                "Quantity on hand cannot be negative.",
                nameof(quantityOnHand));
        }

        QuantityOnHand = quantityOnHand;
    }
}