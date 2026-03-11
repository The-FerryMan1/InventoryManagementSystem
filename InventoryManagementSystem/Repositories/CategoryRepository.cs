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

    public async Task<Category?> DestroyAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if(category is null) return null;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category?> ModifyAsync(int id, UpdateCategoryDto categoryDto)
    {
        var category = await _context.Categories.FindAsync(id);
        if(category is null) return null;

    
        category.Name = categoryDto.Name??category.Name;
        category.Description = categoryDto.Description?? category.Description;
    
        await _context.SaveChangesAsync();
        return category;

    }
}
