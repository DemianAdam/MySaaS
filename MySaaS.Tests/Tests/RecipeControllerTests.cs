using FluentAssertions;
using MySaaS.Application.DTOs.Common.Quantity;
using MySaaS.Application.DTOs.Common.Unities;
using MySaaS.Application.DTOs.Production.Ingredients;
using MySaaS.Application.DTOs.Production.Recipes;
using MySaaS.Application.DTOs.Production.Recipes.Components;
using MySaaS.Tests.Factory;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;


namespace MySaaS.Tests.Tests
{
    public class RecipeControllerTests : BaseIntegrationTest
    {
        public RecipeControllerTests(WebApiFactory factory) : base(factory)
        {

        }

        [Fact]
        public async Task CreateRecipe_WhenCalledWithValidData_ReturnsCreated()
        {
            //Arrange


            HttpClient client = Factory.CreateClient();

            CreateUnitDTO createUnitDTO = new CreateUnitDTO()
            {
                Name = "Gram",
            };

            HttpResponseMessage unitResponse = await client.PostAsJsonAsync("/api/unit", createUnitDTO);
            UnitDTO? unitDTOResponse = await unitResponse.Content.ReadFromJsonAsync<UnitDTO>();
            int idUnit = unitDTOResponse!.Id;

            CreateIngredientDTO createIngredientDTO = new CreateIngredientDTO()
            {
                Name = "Test Ingredient",
            };
            HttpResponseMessage ingredientResponse = await client.PostAsJsonAsync("/api/ingredient", createIngredientDTO);
            IngredientDTO? ingredientDTOResponse = await ingredientResponse.Content.ReadFromJsonAsync<IngredientDTO>();
            int idIngredient = ingredientDTOResponse!.Id;

            CreateRecipeDTO createRecipeDTO = new CreateRecipeDTO()
            {
                Name = "Test Recipe",
                Description = "This is a test recipe",
                RecipeInfo = new CreateRecipeRelationsDTO()
                {
                    Ingredients = new List<CreateRecipeComponentDTO>()
                {
                    new CreateRecipeComponentDTO()
                    {
                        IngredientId = idIngredient,
                        Weight = new CreateQuantityDTO()
                        {
                            Amount = 500,
                            UnitId = idUnit,
                        },
                        Waste = new CreateQuantityDTO()
                        {
                            Amount = 50,
                            UnitId = idUnit,
                        }
                    },

                },
                    Quantity = new CreateQuantityDTO()
                    {
                        Amount = 1000,
                        UnitId = idUnit
                    }
                }
            };


            //act

            var responseRecipe = await client.PostAsJsonAsync("/api/recipe", createRecipeDTO);

            //Assert

            RecipeDTO? recipeDTOResponse = await responseRecipe.Content.ReadFromJsonAsync<RecipeDTO>();
            recipeDTOResponse.Should().NotBeNull();
            recipeDTOResponse.Name.Should().Be(createRecipeDTO.Name);
            recipeDTOResponse.Description.Should().Be(createRecipeDTO.Description);
            recipeDTOResponse.Ingredients.Should().NotBeNull();

            recipeDTOResponse.Ingredients!.Count.Should().Be(createRecipeDTO.RecipeInfo.Ingredients.Count);

            RecipeComponentDTO? ingredient = recipeDTOResponse.Ingredients.FirstOrDefault();

            CreateRecipeComponentDTO? recipeComponent = createRecipeDTO.RecipeInfo.Ingredients.FirstOrDefault();

            ingredient!.Ingredient.Id.Should().Be(recipeComponent!.IngredientId);

            ingredient.Weight.Amount.Should().Be(recipeComponent!.Weight.Amount);
            ingredient.Weight.Unit.Id.Should().Be(recipeComponent!.Weight.UnitId);
            ingredient.Waste.Amount.Should().Be(recipeComponent.Waste.Amount);
            ingredient.Waste.Unit.Id.Should().Be(recipeComponent.Waste.UnitId);

            recipeDTOResponse.Quantity.Amount.Should().Be(createRecipeDTO.RecipeInfo.Quantity.Amount);
            recipeDTOResponse.Quantity.Unit.Id.Should().Be(createRecipeDTO.RecipeInfo.Quantity.UnitId);

        }
    }
}
