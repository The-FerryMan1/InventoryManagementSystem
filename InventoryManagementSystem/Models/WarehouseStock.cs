using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models;

public class WarehouseStock
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;

    public int CurrentQuantity { get; set; }
    
    // Concurrency Token: Prevents two people from updating stock simultaneously
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;

}
