namespace InventoryManagementSystem.Dtos.Product;

public record ProductDto(
    int Id,
    string Sku,
    string Name,
    decimal Price,
    int ReorderLevel,
    int CategoryId
);
