using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySaaS.Application.DTOs.Common.UnitConversions;
using MySaaS.Application.Interfaces.Unities;

namespace MySaaS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitConversionController : ControllerBase
    {
        private readonly IUnitConversionService _unitConversionService;
        public UnitConversionController(IUnitConversionService unitConversionService)
        {
            _unitConversionService = unitConversionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var conversions = await _unitConversionService.GetAllAsync();
            return Ok(conversions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUnitConversionDTO conversionDto)
        {
            var result = await _unitConversionService.AddAsync(conversionDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUnitConversionDTO conversionDto)
        {
            await _unitConversionService.UpdateAsync(conversionDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _unitConversionService.RemoveAsync(id);
            return Ok();
        }
    }
}
