using MySaaS.Application.DTOs.Ingredients;
using MySaaS.Application.Interfaces.Common;

namespace MySaaS.Application.Interfaces.Supplies.Ingredients
{
    public interface IIngredientService : IService<IngredientDTO, CreateIngredientDTO, UpdateIngredientDTO>
    {
    }
}
