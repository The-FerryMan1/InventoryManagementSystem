using InventoryManagementSystem.Dtos.Product;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProduct repo, IProductService productService) : ControllerBase
    {
        private readonly IProduct _repo = repo;
         private readonly IProductService _productService = productService;



        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            try
            {
                 var newProduct = await _productService.CreateProductAsync(createProductDto);
                 return CreatedAtAction(nameof(GetProductById), new{newProduct.Id}, newProduct.ToProductDto());
            }catch(KeyNotFoundException ex)
            {
                return NotFound(new {message = ex.Message});
            }catch(InvalidOperationException ex)
            {
                return Conflict(new {message = ex.Message});
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if(product is null) return NotFound(new {message ="Prodcut does not exists"});

            return Ok(product.ToProductDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
           var products = await _repo.GetAllAsync();
           var productDtos = products.Select(p => p.ToProductDto()).AsEnumerable();

           return Ok(productDtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProduct(int id, [FromBody]UpdateProductDto updateProductDto)
        {

            try
            {
                var updatedProduct = await _productService.ModifyProductAsync(id, updateProductDto);
                return Ok(updatedProduct.ToProductDto());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new {message = ex.Message});
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new {message = ex.Message});
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DestroyProduct(int id)
        {
            var product = await _repo.DestroyAsync(id);
            if(product is null) return NotFound(new {message ="Product does not exist"});
            return NoContent();
        }

    }
}
