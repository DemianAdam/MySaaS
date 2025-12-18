using Dapper;
using MySaaS.Application.Interfaces.Production.Ingredients;
using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production;
using MySaaS.Domain.Entities.Production.Recipes;
using MySaaS.Infrastructure.Database;
using MySaaS.Infrastructure.Mappers;
using MySaaS.Infrastructure.Models;
using MySaaS.Infrastructure.Models.Querys;

namespace MySaaS.Infrastructure.Repositories
{
    internal class IngredientRepository : IIngredientRepository
    {
        private readonly IDapperContext _context;

        public IngredientRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Ingredient obj)
        {
            await _context.Connection.ExecuteAsync(IngredientSQL.Insert,
                new
                {
                    ItemId = obj.ItemId,
                    RecipeId = obj.Recipe?.Id
                },
                _context.Transaction);

            return obj.ItemId;
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            return await _context.Connection.QuerySingleAsync<bool>(IngredientSQL.Exists,
                new
                {
                    ItemId = objId
                },
                _context.Transaction);
        }


        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            var models = await _context.Connection.QueryAsync<IngredientModel>(IngredientSQL.Select, _context.Transaction);
            return models.Map();
        }

        public async Task<Ingredient?> GetByIdAsync(int objId)
        {
            //TODO: add recursive ingredient fetching
            Ingredient? ingredient;
            using (var multiple = await _context.Connection.QueryMultipleAsync(IngredientSQL.SelectByIdWithIngredient, new { Id = objId }, _context.Transaction))
            {
                ingredient = (await multiple.ReadAsync<IngredientModel>()).FirstOrDefault()?.Map();
                if(ingredient is not null && ingredient.Recipe is not null)
                {
                    var components = (await multiple.ReadAsync<RecipeIngredientModel>()).Map().ToList();
                    ingredient.Recipe.UpdateComponents(components);
                }
                
            }
            return ingredient;
        }

        public async Task<int> RemoveAsync(int objId)
        {
            return await _context.Connection.ExecuteAsync(IngredientSQL.Delete,
               new
               {
                   Id = objId
               },
               _context.Transaction);
        }

        public async Task<int> UpdateAsync(Ingredient obj)
        {
            return await _context.Connection.ExecuteAsync(IngredientSQL.Update,
                new
                {
                    ItemId = obj.ItemId,
                    RecipeId = obj.Recipe?.Id
                },
                _context.Transaction);
        }
    }
}
