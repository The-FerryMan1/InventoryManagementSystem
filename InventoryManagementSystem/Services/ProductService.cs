using System;
using InventoryManagementSystem.Dtos.Product;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Wrapper;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Services;

public class ProductService(
    IProduct productRepo,
    ICategory categoryRepo
) : IProductService
{
    private readonly IProduct _productRepo = productRepo;
    private readonly ICategory _categoryRepo = categoryRepo;
    public async Task<Product> CreateProductAsync(CreateProductDto createProductDto)
    {
        var category = await _categoryRepo.GetByIdAsync(createProductDto.CategoryId);

        if (category is null)
        {
            throw new KeyNotFoundException("Category does not exist");
        }

        var newProduct = new Product
        {
            Name = createProductDto.Name,
            Sku = SkuGenerator.Generate(category.Name, createProductDto.Name),
            Price = createProductDto.Price,
            ReorderLevel = createProductDto.ReorderLevel,
            CategoryId = createProductDto.CategoryId,
        };

        var createdProduct = await _productRepo.CreateAsync(newProduct);
        if(createdProduct is null)
        {
            throw new InvalidOperationException("Failed to create new product");
        }

        return createdProduct;
    }

    public async Task<Product> ModifyProductAsync(int id, UpdateProductDto updateProductDto)
    {
        var existingProduct = await _productRepo.GetByIdAsync(id);
        if(existingProduct is null)
        {
           throw new KeyNotFoundException("Product does not exist");
        }

       

        if (updateProductDto.CategoryId.HasValue)
        {   
             var category = await _categoryRepo.GetByIdAsync(updateProductDto.CategoryId.Value);
            if (category is null)
            {
                throw new KeyNotFoundException("New category does not exist");
            } 
        }
       

        var updatedProduct = await _productRepo.ModifyAsync(id, updateProductDto);
        if (updatedProduct is null)
        {
            throw new InvalidOperationException("Failed to update the product");
        }

        return updatedProduct;
    }
}
