using System;
using InventoryManagementSystem.Dtos.Category;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Wrapper;

public static class CategoryDtoWrapper
{
    public static CategoryDto ToCategoryDto(this Category category)
    {
        return new CategoryDto(
            category.Id, 
            category.Name, 
            category.Description);
    }
}
