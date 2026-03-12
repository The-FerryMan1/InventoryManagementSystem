using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Dtos.Warehouse;

public record CreateWarehouseDto(

    [Required, StringLength(100, MinimumLength = 5)]
    string Name,
    [Required, StringLength(255, MinimumLength = 5)]
    string Location
);