using System;
using System.Security.Cryptography.X509Certificates;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Dtos.Product;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Repositories;

public class ProductRepository(
    ApplicationDbContext context
    ) : IProduct
{   
    private readonly ApplicationDbContext _context = context;
    public async Task<Product?> CreateAsync(Product newProduct)
    {
        
        await _context.Products.AddAsync(newProduct);
        await _context.SaveChangesAsync();

        return newProduct;
    }

    public async Task<Product?> DestroyAsync(int id)
    {
        var product = await GetByIdAsync(id);
        if(product is null) return null;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id); 
    }

    public async Task<Product?> ModifyAsync(int id, UpdateProductDto updateProductDto)
    {
        var product  = await GetByIdAsync(id);
        if(product is null) return null;

        product.Name = updateProductDto.Name??product.Name;
        product.Price = updateProductDto.Price??product.Price;
        product.ReorderLevel = updateProductDto.ReorderLevel??product.ReorderLevel;
        product.CategoryId = updateProductDto.CategoryId??product.CategoryId;

        await _context.SaveChangesAsync();
        return product;


    }
}
