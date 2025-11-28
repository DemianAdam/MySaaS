using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySaaS.Application.DTOs.Supplies;
using MySaaS.Application.Interfaces.Supplies;

namespace MySaaS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyController : ControllerBase
    {
        private readonly ISupplyService _service;

        public SupplyController(ISupplyService supplyService)
        {
            _service = supplyService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplyDTO supplyDto)
        {
            await _service.AddAsync(supplyDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var supplies = await _service.GetAllAsync();
            return Ok(supplies);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SupplyDTO supplyDto)
        {
            await _service.UpdateAsync(supplyDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.RemoveAsync(id);
            return Ok();
        }

    }
}
