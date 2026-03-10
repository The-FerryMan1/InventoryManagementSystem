using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Dtos.Category;

public class CreateCategoryDto
{   
    [Required, StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }
    [Required, StringLength(255)]
    public string? Description { get; set; } = string.Empty;
}
