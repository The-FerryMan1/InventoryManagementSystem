using System;

namespace InventoryManagementSystem.Models;

public enum TransactionType
{
    StockIn,    // Purchase from Supplier
    StockOut,   // Sale to Customer
    Adjustment, // Manual correction (e.g., damaged goods)
    Transfer    // Between Warehouses
}

public class StockTransaction
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int WarehouseId { get; set; }
    public int Quantity { get; set; } // Use negative for StockOut
    
    public TransactionType Type { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    
    public string? Reason { get; set; } // e.g., "Order #1234"
    public string? RecordedBy { get; set; } // User ID
}
