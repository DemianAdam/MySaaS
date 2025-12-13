using MySaaS.Application.DTOs.Production.Ingredients;
using MySaaS.Application.Interfaces.Base;

namespace MySaaS.Application.Interfaces.Production.Ingredients
{
    public interface IIngredientService : IService<IngredientDTO, CreateIngredientDTO, UpdateIngredientDTO>
    {
    }
}
