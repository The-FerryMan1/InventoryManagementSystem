using InventoryManagementSystem.Dtos.Warehouse;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController(IWarehouse repo) : ControllerBase
    {
        private readonly IWarehouse _repo = repo;

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseDto warehouseDto)
        {
            var newWarehouse = await _repo.CreateAsync(warehouseDto);
            if(newWarehouse is null) return StatusCode(500, "Internal Service Error");
            return CreatedAtAction(
                nameof(GetWarehouseById),
                new { newWarehouse.Id },
                newWarehouse.ToWarehouseDto()
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            var warehouse = await _repo.GetByIdAsync(id);
            if(warehouse is null) return NotFound(new {message = "warehouse does not exist"});
            return Ok(warehouse.ToWarehouseDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warehouses =  await _repo.GetAllAsync();
            var warehousesDto = warehouses.Select(w => w.ToWarehouseDto()).AsEnumerable();
            return Ok(warehousesDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyWarehouse(int id, [FromBody] UpdateWarehouseDto updateWarehouseDto)
        {
            var warehouse = await _repo.ModifyAsync(id, updateWarehouseDto);
            if(warehouse is null) return NotFound(new {message = "Warehouse does not exist"});

            return Ok(warehouse.ToWarehouseDto());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
         public async Task<IActionResult> DestroyWarehouse(int id)
        {
            var warehouse = await _repo.DestroyAsync(id);
            if(warehouse is null) return NotFound(new {message ="Warehouse does not exist"});
            return NoContent();
        }
    }   
}
