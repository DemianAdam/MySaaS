using MySaaS.Application.Interfaces.Common;
using MySaaS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Application.Interfaces.Items.Ingredients
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
    }
}
