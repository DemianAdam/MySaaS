using MySaaS.Application.DTOs.Items.Ingredients;
using MySaaS.Application.Interfaces.Common;

namespace MySaaS.Application.Interfaces.Items.Ingredients
{
    public interface IIngredientService : IService<IngredientDTO, CreateIngredientDTO, UpdateIngredientDTO>
    {
    }
}
