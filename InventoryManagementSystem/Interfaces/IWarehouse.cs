using System;
using InventoryManagementSystem.Dtos.Warehouse;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Interfaces;

public interface IWarehouse
{
    Task<List<Warehouse>> GetAllAsync();
    Task<Warehouse?> GetByIdAsync(int id);
    Task<Warehouse?> CreateAsync(CreateWarehouseDto warehouseDto);
    Task<Warehouse?> ModifyAsync(int id, UpdateWarehouseDto warehouseDto);
    Task<Warehouse?> DestroyAsync(int id);
}
