using MySaaS.Application.DTOs.Production.Recipes;
using MySaaS.Application.Interfaces.Common;

namespace MySaaS.Application.Interfaces.Recipes
{
    public interface IRecipeService : IService<RecipeDTO,CreateRecipeDTO,UpdateRecipeDTO>
    {
        Task<IEnumerable<RecipeDTO>> GetAllWithIngredientsAsync();
    }
}
