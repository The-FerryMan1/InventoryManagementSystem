using System;

namespace InventoryManagementSystem.Helpers;

public static class SkuGenerator
{
    public static string Generate(string categoryName, string productName)
    {
        string catPart = categoryName[..Math.Min(3, categoryName.Length)].ToUpper();
        string prodPart =  productName[..Math.Min(3, productName.Length)].ToUpper();
        string uniquePart = DateTime.UtcNow.Ticks.ToString()[12..];

        return $"{catPart}-{prodPart}-{uniquePart}";
    }
}
