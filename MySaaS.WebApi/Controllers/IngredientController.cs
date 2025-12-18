using Microsoft.AspNetCore.Mvc;
using MySaaS.Application.DTOs.Production.Ingredients;
using MySaaS.Application.Interfaces.Production.Ingredients;
namespace MySaaS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _service;
        private readonly ILogger<IngredientController> _logger;

        public IngredientController(
            IIngredientService ingredientService,
            ILogger<IngredientController> logger)
        {
            _service = ingredientService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIngredientDTO ingredientDto)
        {
            IngredientResponse result;
            try
            {
                result = await _service.AddAsync(ingredientDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ingredient: {IngredientName}", ingredientDto.Name);
                return StatusCode(500, "An error occurred while creating the ingredient.");
            }
            _logger.LogInformation("Ingredient created: {IngredientName}", ingredientDto.Name);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IngredientDTO>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var ingredients = await _service.GetAllAsync();
            return Ok(ingredients);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateIngredientDTO ingredientDto)
        {
            await _service.UpdateAsync(ingredientDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromQuery] bool deleteRecipe = false)
        {
            await _service.RemoveAsync(id);

            return Ok();
        }
    }
}
