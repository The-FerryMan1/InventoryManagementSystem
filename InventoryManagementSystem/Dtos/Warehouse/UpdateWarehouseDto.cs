using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Dtos.Warehouse;

public record UpdateWarehouseDto(
    [StringLength(100, MinimumLength = 5)]
    string? Name,
    [StringLength(255, MinimumLength = 5)]
    string? Location
);
