using System;
using InventoryManagementSystem.Dtos.Warehouse;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Wrapper;

public static class WarehouseWrapper
{
    public static WarehouseDto ToWarehouseDto(this Warehouse warehouse)
    {
        return new WarehouseDto
        (
            warehouse.Id,
            warehouse.Name,
            warehouse.Location
        );
    }
}
