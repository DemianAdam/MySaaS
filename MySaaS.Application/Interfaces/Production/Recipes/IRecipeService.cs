using MySaaS.Application.DTOs.Production.Recipes;
using MySaaS.Application.Interfaces.Base;

namespace MySaaS.Application.Interfaces.Production.Recipes
{
    public interface IRecipeService : IService<RecipeDTO,CreateRecipeDTO,UpdateRecipeDTO,RecipeResponse>
    {
        Task<IEnumerable<RecipeDTO>> GetAllWithIngredientsAsync();
    }
}
