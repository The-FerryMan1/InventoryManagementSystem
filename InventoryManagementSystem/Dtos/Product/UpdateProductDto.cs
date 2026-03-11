using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Dtos.Product;

public record  UpdateProductDto(
    [StringLength(100, MinimumLength = 5)]
    string? Name,
    [Range(0.01, 1000000.00)]
    decimal? Price,
    int? ReorderLevel,
    int? CategoryId
);

