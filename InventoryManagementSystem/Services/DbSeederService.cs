
using InventoryManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
namespace InventoryManagementSystem.Services;

public class DbSeederService
{
   public static async Task SeedAdminUser(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
   
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


    await context.Database.EnsureCreatedAsync();

    if(!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var adminEmail = "admin@example.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var newUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        var result = await userManager.CreateAsync(newUser, "StrongPassword123!");
    
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newUser, "Admin");
        }
    }
}
}
