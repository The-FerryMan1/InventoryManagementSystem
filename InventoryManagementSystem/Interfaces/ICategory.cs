using System;
using InventoryManagementSystem.Dtos.Category;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Interfaces;

public interface ICategory
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category?> CreateAsync(CreateCategoryDto categoryDto);
    Task<Category?> ModifyAsync(int id, UpdateCategoryDto categoryDto);
    Task<Category?> DestroyAsync(int id);


}
