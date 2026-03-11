using System;
using InventoryManagementSystem.Dtos.Product;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Interfaces;

public interface IProductService
{
    Task<Product> CreateProductAsync(CreateProductDto createProductDto);
    Task<Product> ModifyProductAsync(int id, UpdateProductDto updateProductDto);
}
