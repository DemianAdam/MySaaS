using MySaaS.Application.Interfaces.Base;
using MySaaS.Domain.Entities.Production.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.Interfaces.Production.Recipes
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<IEnumerable<Recipe>> GetAllWithIngredientsAsync();
    }
}
