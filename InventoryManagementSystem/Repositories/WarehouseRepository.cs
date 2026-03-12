using System;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Dtos.Warehouse;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Repositories;

public class WarehouseRepository(ApplicationDbContext context) : IWarehouse
{
    private readonly ApplicationDbContext _context = context;
    public async Task<Warehouse?> CreateAsync(CreateWarehouseDto warehouseDto)
    {
        var newWarehouse = new Warehouse
        {
            Name = warehouseDto.Name,
            Location = warehouseDto.Location
        };

        await _context.Warehouses.AddAsync(newWarehouse);
        await _context.SaveChangesAsync();

        return newWarehouse;
    }

    public async Task<Warehouse?> DestroyAsync(int id)
    {
        var warehouse  = await GetByIdAsync(id);
        if(warehouse is null) return null;
         _context.Warehouses.Remove(warehouse);
         await _context.SaveChangesAsync();
        return warehouse;

    }

    public async Task<List<Warehouse>> GetAllAsync()
    {
        return await _context.Warehouses.ToListAsync();;
    }

    public async Task<Warehouse?> GetByIdAsync(int id)
    {
       return await _context.Warehouses.FindAsync(id);
    }

    public async Task<Warehouse?> ModifyAsync(int id, UpdateWarehouseDto warehouseDto)
    {
        var warehouse = await GetByIdAsync(id);
        if(warehouse is null) return null;

        warehouse.Name = warehouseDto.Name??warehouse.Name; 
        warehouse.Location = warehouseDto.Location??warehouse.Location;

        await _context.SaveChangesAsync();
        return warehouse;
    }
}
