using Dapper;
using MySaaS.Application.Interfaces.Supplies.Ingredients;
using MySaaS.Domain.Entities;
using MySaaS.Domain.Entities.Recipes;
using MySaaS.Infrastructure.Database;

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
            string sql =
                """
                    INSERT INTO Ingredients 
                    (id_supply, id_recipe)
                    VALUES
                    (@SupplyId, @RecipeId);
                """;

            await _context.Connection.ExecuteAsync(sql, new { SupplyId = obj.SupplyId, RecipeId = obj.Recipe?.Id }, _context.Transaction);

            return obj.SupplyId;
        }

        public async Task<bool> ExistsAsync(int objId)
        {
            string sql =
                """
                SELECT EXISTS (
                    SELECT 1
                    FROM supplies
                    WHERE supply_id = @SupplyId
                );
                """;
            return await _context.Connection.QuerySingleAsync<bool>(sql, new { Id = objId }, _context.Transaction);
        }


        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            string selectIngredients =
                $"""
                    SELECT 
                        s.supply_id AS {nameof(Ingredient.Supply.Id)},
                        s.name AS {nameof(Ingredient.Supply.Name)},
                        s.description AS {nameof(Ingredient.Supply.Description)},
                        r.recipe_id AS {nameof(Ingredient.Recipe.Id)},
                        r.name AS {nameof(Ingredient.Recipe.Name)},
                        r.quantity_unit_id AS {nameof(Ingredient.Recipe.Quantity.UnitId)},
                        r.quantity AS {nameof(Ingredient.Recipe.Quantity.Amount)},
                        u.unit_id AS {nameof(Ingredient.Recipe.Quantity.Unit.Id)},
                        u.name AS {nameof(Ingredient.Recipe.Quantity.Unit.Name)}
                    FROM ingredients AS i
                    LEFT JOIN supplies AS s ON s.supply_id = i.id_supply
                    LEFT JOIN recipes AS r ON r.recipe_id = i.id_recipe
                    LEFT JOIN unities AS u ON u.unit_id = r.quantity_unit_id
                """;
            return await _context.Connection.QueryAsync<Supply, Recipe, Quantity, Unit, Ingredient>(selectIngredients, (s, r, q, u) =>
            {
                if (r is not null)
                {
                    r.Quantity = q;
                    r.Quantity.Unit = u;
                }

                return new Ingredient
                {
                    SupplyId = s.Id,
                    Supply = s,
                    Recipe = r
                };
            }, _context.Transaction,
            splitOn: $"{nameof(Ingredient.Recipe.Id)},{nameof(Ingredient.Recipe.Quantity.UnitId)},{nameof(Ingredient.Recipe.Quantity.Unit.Id)}");
        }

        public async Task<Ingredient?> GetByIdAsync(int objId)
        {
            string sql =
                $"""
                    SELECT 
                        SELECT 
                        s.supply_id AS {nameof(Ingredient.Supply.Id)},
                        s.name AS {nameof(Ingredient.Supply.Name)},
                        s.description AS {nameof(Ingredient.Supply.Description)},
                        r.recipe_id AS {nameof(Ingredient.Recipe.Id)},
                        r.name AS {nameof(Ingredient.Recipe.Name)},
                        r.quantity_unit_id AS {nameof(Ingredient.Recipe.Quantity.UnitId)},
                        r.quantity AS {nameof(Ingredient.Recipe.Quantity.Amount)},
                        u.unit_id AS {nameof(Ingredient.Recipe.Quantity.Unit.Id)},
                        u.name AS {nameof(Ingredient.Recipe.Quantity.Unit.Name)}
                    FROM ingredients AS i
                    JOIN supplies AS s ON s.supply_id = i.id_supply
                    LEFT JOIN recipes AS r ON r.recipe_id = i.id_recipe
                    LEFT JOIN unities AS u ON u.unit_id = r.quantity_unit_id
                    WHERE i.id_supply = @Id
                """;
            //TODO: maybe add recursive ingredient fetching?
            var result = await _context.Connection.QueryAsync<Supply, Recipe, Quantity, Unit, Ingredient>(sql, (s, r, q, u) =>
             {
                 if (r is not null)
                 {
                     r.Quantity = q;
                     r.Quantity.Unit = u;
                 }

                 return new Ingredient
                 {
                     SupplyId = s.Id,
                     Supply = s,
                     Recipe = r
                 };
             }, _context.Transaction,
             splitOn: $"{nameof(Ingredient.Recipe.Id)},{nameof(Ingredient.Recipe.Quantity.UnitId)},{nameof(Ingredient.Recipe.Quantity.Unit.Id)}");

            return result.FirstOrDefault();
        }

        public async Task<int> RemoveAsync(int objId)
        {
            string sql =
                """
                    DELETE FROM ingredients 
                    WHERE id_supply = @Id
                """;

            return await _context.Connection.ExecuteAsync(sql,
               new
               {
                   Id = objId
               }, _context.Transaction);
        }

        public async Task<int> UpdateAsync(Ingredient obj)
        {
            string sql =
                """
                    UPDATE ingredients
                    SET id_recipe = @RecipeId
                    WHERE id_supply = @SupplyId
                """;
            return await _context.Connection.ExecuteAsync(sql, new { SupplyId = obj.SupplyId, RecipeId = obj.Recipe?.Id }, _context.Transaction);
        }
    }
}
