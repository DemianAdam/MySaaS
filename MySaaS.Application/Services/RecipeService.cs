using MySaaS.Application.DTOs.Production.Recipes;
using MySaaS.Application.Interfaces.Base;
using MySaaS.Application.Interfaces.Common.Items;
using MySaaS.Application.Interfaces.Production.Recipes;
using MySaaS.Application.Mappers;
using MySaaS.Domain.Entities.Common;
using MySaaS.Domain.Entities.Production.Recipes;
using MySaaS.Domain.Exceptions.Common;

namespace MySaaS.Application.Services
{
    internal class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RecipeService(
            IRecipeRepository recipeRepository,
            IItemRepository itemRepository,
            IUnitOfWork unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<RecipeResponse> AddAsync(CreateRecipeDTO obj)
        {
            Recipe recipe = obj.Map();
            if (recipe.Item is null)
            {
                throw new ArgumentException("Recipe must have an associated Item.");
            }

            _unitOfWork.BeginTransaction();
            try
            {
                int itemId = await _itemRepository.AddAsync(recipe.Item);
                recipe.Id = itemId;

                int recipeId = await _recipeRepository.AddAsync(recipe);
                _unitOfWork.Commit();
               

                return recipe.ToResponse();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            
        }

        public async Task<IEnumerable<RecipeDTO>> GetAllAsync()
        {
            var recipes = await _recipeRepository.GetAllAsync();
            return recipes.Map();
        }

        public async Task<IEnumerable<RecipeDTO>> GetAllWithIngredientsAsync()
        {
            var recipes = await _recipeRepository.GetAllWithIngredientsAsync();
            return recipes.Map();
        }

        public async Task RemoveAsync(int objId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                int affected = await _recipeRepository.RemoveAsync(objId);
                if (affected == 0)
                {
                    throw new NotFoundException<Recipe>(objId);
                }
                await _itemRepository.RemoveAsync(objId);
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }

        public async Task<RecipeResponse> UpdateAsync(UpdateRecipeDTO obj)
        {
            Recipe recipe = obj.Map();

            if (recipe.Item is null)
            {
                throw new ArgumentException("Recipe must have an associated Item.");
            }

            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                int affected = await _itemRepository.UpdateAsync(recipe.Item);
                if (affected == 0)
                {
                    throw new NotFoundException<Item>(obj.Id);
                }

                await _recipeRepository.UpdateAsync(recipe);
                transaction.Commit();
                return recipe.ToResponse();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
