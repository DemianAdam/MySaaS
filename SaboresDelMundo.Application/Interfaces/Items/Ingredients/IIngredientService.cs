using MySaaS.Application.DTOs.Production.Ingredients;
using MySaaS.Application.Interfaces.Common;

namespace MySaaS.Application.Interfaces.Items.Ingredients
{
    public interface IIngredientService : IService<IngredientDTO, CreateIngredientDTO, UpdateIngredientDTO>
    {
    }
}
