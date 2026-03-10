using System;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Dtos.Category;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategory
{      
    private readonly ApplicationDbContext _context = context;

    public async Task<Category?> CreateAsync(CreateCategoryDto categoryDto)
    {
        var newCategory = new Category
        {
            Name = categoryDto.Name,
            Description = categoryDto.Description
        };
        await _context.AddAsync(newCategory);
        await _context.SaveChangesAsync();
        return newCategory;
    
    }

    public Task<Category?> DestroyAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public Task<Category?> ModifyAsync(int id, UpdateCategoryDto categoryDto)
    {
        throw new NotImplementedException();
    }
}
