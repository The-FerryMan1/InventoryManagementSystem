using System;
using InventoryManagementSystem.Dtos.Product;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Wrapper;

public static class ProductDtoWrapper
{
    public static ProductDto ToProductDto(this Product product)
    {
        return new ProductDto(
            product.Id, 
            product.Sku, 
            product.Name, 
            product.Price, 
            product.ReorderLevel, 
            product.CategoryId);
    }
}

