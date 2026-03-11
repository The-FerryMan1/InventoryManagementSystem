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
    public class CategoryController(
        ICategory repo,
        ILogger<CategoryController> logger
        ) : ControllerBase
    {
        
        private readonly ICategory _repo = repo;
        private readonly ILogger _logger = logger;

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

            if(newCategory is null)
            {
                _logger.LogCritical("An unexpected error occured while creating a category");
                return StatusCode(500);
            }

            return CreatedAtAction(nameof(GetCategoryById), new{id = newCategory.Id}, newCategory.ToCategoryDto());
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyCategory(int id, [FromBody] UpdateCategoryDto category)
        {
            var updatedCategory = await _repo.ModifyAsync(id, category);
            if(updatedCategory is null) return NotFound(new {message = "No category was found"});

            return Ok(updatedCategory.ToCategoryDto());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DestroyCategory(int id)
        {
            var category = await _repo.DestroyAsync(id);
            if(category is null) return NotFound(new {message = "Category was not found"});

            return NoContent();
        }
    
    }
}
