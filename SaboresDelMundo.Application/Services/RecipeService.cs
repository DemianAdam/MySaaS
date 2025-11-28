using MySaaS.Application.DTOs.Recipes;
using MySaaS.Application.Interfaces.Recipes;
using MySaaS.Application.Mappers;
using MySaaS.Domain.Entities.Recipes;
using MySaaS.Domain.Exceptions.Common;

namespace MySaaS.Application.Services
{
    internal class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public Task<int> AddAsync(CreateRecipeDTO obj)
        {
            Recipe recipe = obj.Map();
            return _recipeRepository.AddAsync(recipe);
        }

        public async Task<IEnumerable<RecipeDTO>> GetAllAsync()
        {
            var recipes = await _recipeRepository.GetAllAsync();
            return recipes.Map();
        }

        public async Task<IEnumerable<RecipeDTO>> GetAllWithIngredientsAsync()
        {
            var recipes = await _recipeRepository.GetAllWithIngredientsAsync();
            return recipes.Map();
        }

        public async Task RemoveAsync(int objId)
        {
            int affected = await _recipeRepository.RemoveAsync(objId);
            if (affected == 0)
            {
                throw new NotFoundException<Recipe>(objId);
            }
        }

        public async Task UpdateAsync(UpdateRecipeDTO obj)
        {
            Recipe recipe = obj.Map();
            int affected = await _recipeRepository.UpdateAsync(recipe);
            if (affected == 0)
            {
                throw new NotFoundException<Recipe>(obj.Id);
            }
        }
    }
}
