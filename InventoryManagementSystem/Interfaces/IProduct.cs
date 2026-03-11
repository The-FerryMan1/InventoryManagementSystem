using System;
using InventoryManagementSystem.Dtos.Product;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Interfaces;

public interface IProduct
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> CreateAsync(Product newProduct);
    Task<Product?> ModifyAsync(int id,  UpdateProductDto updateProductDto);
    Task<Product?> DestroyAsync(int id);
}
