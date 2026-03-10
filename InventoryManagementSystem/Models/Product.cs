using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models;

public class Product
{
    public int Id { get; set; }
    [Required, StringLength(50)]
    public string Sku { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int ReorderLevel { get; set; }

    //relationship
    public int CategoryId {get; set;}
    public Category? Category { get; set; } 

    public ICollection<WarehouseStock> WarehouseStocks {get; set;} = [];
}
