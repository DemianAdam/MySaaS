using MySaaS.Application.Mappers;
using Microsoft.Extensions.Logging;
using MySaaS.Domain.Exceptions.Common;
using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production;
using MySaaS.Application.DTOs.Production.Ingredients;
using MySaaS.Application.Interfaces.Common.Items;
using MySaaS.Application.Interfaces.Base;
using MySaaS.Application.Interfaces.Production.Ingredients;
using MySaaS.Application.Interfaces.Production.Recipes;


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
        public async Task<IngredientResponse> AddAsync(CreateIngredientDTO obj)
        {
            Ingredient ingredient = obj.Map();

            if (ingredient.Item is null)
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
                return ingredient.ToResponse();
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
            _unitOfWork.BeginTransaction();
            try
            {
                int affected = await _ingredientRepository.RemoveAsync(objId);
                if (affected == 0)
                {
                    throw new NotFoundException<Ingredient>(objId);
                }
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

        public async Task<IngredientResponse> UpdateAsync(UpdateIngredientDTO obj)
        {
            Ingredient ingredient = obj.Map();

            if (ingredient.Item is null)
            {
                throw new ArgumentException("Ingredient must have an associated Item.");
            }

            _unitOfWork.BeginTransaction();
            try
            {
                int affected = await _itemRepository.UpdateAsync(ingredient.Item);
                if (affected == 0)
                {
                    throw new NotFoundException<Item>(ingredient.ItemId);
                }

                if (ingredient.Recipe is not null)
                {
                    await _recipeRepository.UpdateAsync(ingredient.Recipe);
                }

                await _ingredientRepository.UpdateAsync(ingredient);

                _unitOfWork.Commit();
                return ingredient.ToResponse();
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
