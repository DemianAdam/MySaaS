using MySaaS.Application.Interfaces.Common;
using MySaaS.Application.Mappers;
using MySaaS.Domain.Entities;
using Microsoft.Extensions.Logging;
using MySaaS.Application.Interfaces.Recipes;
using MySaaS.Domain.Exceptions.Common;
using MySaaS.Application.Interfaces.Items;
using MySaaS.Application.DTOs.Items.Ingredients;
using MySaaS.Application.Interfaces.Items.Ingredients;


namespace MySaaS.Application.Services
{
    internal class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        private readonly IItemRepository _itemRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public IngredientService(
            IIngredientRepository ingredientRepository,
            IRecipeRepository recipeRepository,
            IUnitOfWork unitOfWork,
            IItemRepository itemRepository,
            ILogger<IngredientService> logger)
        {
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<int> AddAsync(CreateIngredientDTO obj)
        {
            Ingredient ingredient = obj.Map();

            if(ingredient.Item is null)
            {
                throw new ArgumentException("Ingredient must have an associated Item.");
            }

            _unitOfWork.BeginTransaction();
            try
            {
                int itemId = await _itemRepository.AddAsync(ingredient.Item);
                ingredient.ItemId = itemId;

                if (ingredient.Recipe is not null)
                {
                    int recipeId = await _recipeRepository.AddAsync(ingredient.Recipe);
                    ingredient.Recipe.Id = recipeId;
                }

                int ingredientId = await _ingredientRepository.AddAsync(ingredient);

                _unitOfWork.Commit();
                return ingredientId;
            }
            catch
            {
                _logger.LogError("Error occurred while adding a new ingredient. Rolling back transaction.");
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IEnumerable<IngredientDTO>> GetAllAsync()
        {
            IEnumerable<Ingredient> ingredients = await _ingredientRepository.GetAllAsync();

            return ingredients.Map();
        }

        public async Task RemoveAsync(int objId)
        {
            bool exists = await _ingredientRepository.ExistsAsync(objId);
            if (!exists)
            {
                throw new NotFoundException<Ingredient>(objId);
            }

            _unitOfWork.BeginTransaction();
            try
            {
                await _ingredientRepository.RemoveAsync(objId);
                await _itemRepository.RemoveAsync(objId);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _logger.LogError("Error occurred while removing an ingredient. Rolling back transaction.");
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task UpdateAsync(UpdateIngredientDTO obj)
        {
            Ingredient ingredient = obj.Map();

            if(ingredient.Item is null)
            {
                throw new ArgumentException("Ingredient must have an associated Item.");
            }

            bool exists = await _ingredientRepository.ExistsAsync(obj.Id);

            if (!exists)
            {
                throw new NotFoundException<Ingredient>(obj.Id);
            }

            _unitOfWork.BeginTransaction();
            try
            {
                await _itemRepository.UpdateAsync(ingredient.Item);

                if (ingredient.Recipe is not null)
                {
                    await _recipeRepository.UpdateAsync(ingredient.Recipe);
                }

                await _ingredientRepository.UpdateAsync(ingredient);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating an ingredient. Rolling back transaction.");
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }
    }
}
