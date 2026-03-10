using System;

namespace InventoryManagementSystem.Dtos.Category;

public record CategoryDto(
    int Id,
    string Name,
    string? Description
);
