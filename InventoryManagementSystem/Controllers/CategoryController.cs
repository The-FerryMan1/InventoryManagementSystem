using InventoryManagementSystem.Dtos.Category;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{   
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategory repo) : ControllerBase
    {
        
        private readonly ICategory _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _repo.GetAllAsync();
            var categoriesDto =  categories.Select(c=> c.ToCategoryDto()).ToList();
            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if(category is null) return NotFound(new {message = "Category was not found"});

            return Ok(category.ToCategoryDto());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto category)
        {
            var newCategory = await _repo.CreateAsync(category);

            return CreatedAtAction(nameof(GetCategoryById), new{id = newCategory.Id}, newCategory.ToCategoryDto());
        }

    }
}
