using MySaaS.Application.DTOs.Recipes;
using MySaaS.Application.Interfaces.Common;
using MySaaS.Domain.Entities.Recipes;

namespace MySaaS.Application.Interfaces.Recipes
{
    public interface IRecipeService : IService<RecipeDTO,CreateRecipeDTO,UpdateRecipeDTO>
    {
        Task<IEnumerable<RecipeDTO>> GetAllWithIngredientsAsync();
    }
}
