using MySaaS.Application.DTOs.Ingredients;
using MySaaS.Application.Interfaces.Common;
using MySaaS.Application.Interfaces.Supplies;
using MySaaS.Application.Interfaces.Supplies.Ingredients;
using MySaaS.Application.Mappers;
using MySaaS.Domain.Entities;
using Microsoft.Extensions.Logging;
using MySaaS.Application.Interfaces.Recipes;
using MySaaS.Domain.Exceptions.Common;


namespace MySaaS.Application.Services
{
    internal class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ISupplyRepository _supplyRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public IngredientService(
            IIngredientRepository ingredientRepository,
            ISupplyRepository supplyRepository,
            IRecipeRepository recipeRepository,
            IUnitOfWork unitOfWork,
            ILogger<IngredientService> logger)
        {
            _ingredientRepository = ingredientRepository;
            _supplyRepository = supplyRepository;
            _recipeRepository = recipeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<int> AddAsync(CreateIngredientDTO obj)
        {
            Ingredient ingredient = obj.Map();

            if (ingredient.Supply is null)
            {
                throw new ArgumentException("Ingredient must have an associated Supply.");
            }

            _unitOfWork.BeginTransaction();
            try
            {
                int supplyId = await _supplyRepository.AddAsync(ingredient.Supply);
                ingredient.SupplyId = supplyId;

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
                await _supplyRepository.RemoveAsync(objId);

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
            if (ingredient.Supply is null)
            {
                throw new ArgumentException("Ingredient must have an associated Supply.");
            }

            bool exists = await _ingredientRepository.ExistsAsync(obj.Id);

            if (!exists)
            {
                throw new NotFoundException<Ingredient>(obj.Id);
            }

            _unitOfWork.BeginTransaction();
            try
            {
                await _supplyRepository.UpdateAsync(ingredient.Supply);


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
