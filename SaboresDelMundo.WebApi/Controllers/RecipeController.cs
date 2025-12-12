using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySaaS.Application.DTOs.Production.Recipes;
using MySaaS.Application.Interfaces.Recipes;

namespace MySaaS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _service;
        public RecipeController(IRecipeService recipeService)
        {
            _service = recipeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeDTO recipeDto)
        {
            var result =await _service.AddAsync(recipeDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeIngredients = false)
        {
            IEnumerable<RecipeDTO> recipes;
            if(includeIngredients)
            {
                recipes = await _service.GetAllWithIngredientsAsync();
                return Ok(recipes);
            }

            recipes = await _service.GetAllAsync();
            return Ok(recipes);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRecipeDTO recipeDto)
        {
            await _service.UpdateAsync(recipeDto);
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
