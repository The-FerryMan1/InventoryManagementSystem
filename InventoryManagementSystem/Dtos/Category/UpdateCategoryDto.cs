using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Dtos.Category;

public class UpdateCategoryDto
{   
    [StringLength(100, MinimumLength = 3)]
    public string? Name { get; set; } = string.Empty;
    [StringLength(100, MinimumLength = 3)]
    public string? Description {get; set;} = string.Empty;
}
