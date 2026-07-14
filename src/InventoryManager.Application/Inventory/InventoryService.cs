using InventoryManager.Application.Common.Interfaces;
using InventoryManager.Application.Warehouses;
using InventoryManager.Domain.Warehouses;

namespace InventoryManager.Application.Inventory;

public class InventoryService : IInventoryService
{
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IWarehouseStockRepository _warehouseStockRepository;

    public InventoryService(
        IProductRepository productRepository,
        IWarehouseRepository warehouseRepository,
        IWarehouseStockRepository warehouseStockRepository)
    {
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
        _warehouseStockRepository = warehouseStockRepository;
    }

    public async Task<WarehouseStockReponse> SetStockAsync(
        int productId,
        int warehouseId,
        SetWarehouseStockRequest request,
        CancellationToken cancellationToken)
    {
        var productExists = await _productRepository.ExistsAsync(
            productId,
            cancellationToken);

        if (!productExists)
        {
            throw new ArgumentException(
                $"Product with ID '{productId}' does not exist.",
                nameof(productId));
        }

        var warehouseExists = await _warehouseRepository.ExistsAsync(
            warehouseId,
            cancellationToken);

        if (!warehouseExists)
        {
            throw new ArgumentException(
                $"Warehouse with ID '{warehouseId}' does not exist.",
                nameof(warehouseId));
        }

        var warehouseStock = await _warehouseStockRepository.GetByProductAndWarehouseAsync(
            productId,
            warehouseId,
            cancellationToken);

        if (warehouseStock is null)
        {
            warehouseStock = new WarehouseStock(
                productId,
                warehouseId,
                request.QuantityOnHand);

            await _warehouseStockRepository.AddAsync(
                warehouseStock,
                cancellationToken);
        }
        else
        {
            warehouseStock.SetQuantity(request.QuantityOnHand);

            await _warehouseStockRepository.SaveChangesAsync(
                cancellationToken);
        }

        return MapToResponse(warehouseStock);
    }

    private static WarehouseStockReponse MapToResponse(
        WarehouseStock warehouseStock)
    {
        return new WarehouseStockReponse(
            warehouseStock.ProductId,
            warehouseStock.WarehouseId,
            warehouseStock.QuantityOnHand);
    }
}