using InventoryManagementSystem.Data;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString"));

});



builder.Services.AddControllers();

builder.Services.AddAuthorization();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{  
   #region  --Password settings-- 
   options.Password.RequireDigit = true;
   options.Password.RequiredLength = 8;
   options.Password.RequiredUniqueChars = 1;
   options.Password.RequireLowercase = true;
   options.Password.RequireUppercase = true;
   #endregion

   #region --Lockout settings--
   options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
   options.Lockout.MaxFailedAccessAttempts = 5;
   options.Lockout.AllowedForNewUsers = true;
   #endregion

   #region --User settings--
   options.User.RequireUniqueEmail = true;
   #endregion
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax; // Allows cross-site navigation if needed
    options.LoginPath = "/login"; // Ensure this matches your route
    options.Events.OnRedirectToReturnUrl = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;  
    };
});

builder.Services.AddOpenApi();

builder.Services.AddAntiforgery();

builder.Services.AddScoped<ICategory, CategoryRepository>();

var app = builder.Build();

// database seeder
await DbSeederService.SeedAdminUser(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.MapScalarApiReference();
}

//middlewares
app.UseAntiforgery();

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();  

app.MapControllers();



app.Run();


