using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySaaS.Application.DTOs.Common.Unities;
using MySaaS.Application.Interfaces.Unities;
using MySaaS.Application.Services;

namespace MySaaS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _service;
        public UnitController(IUnitService unityService)
        {
            _service = unityService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUnitDTO unityDto)
        {
            await _service.AddAsync(unityDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUnitDTO unityDto)
        {
            await _service.UpdateAsync(unityDto);
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
