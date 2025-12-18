using FluentAssertions;
using MySaaS.Application.DTOs.Common.Quantity;
using MySaaS.Application.DTOs.Common.Unities;
using MySaaS.Application.DTOs.Production.Ingredients;
using MySaaS.Application.DTOs.Production.Recipes.Components;
using MySaaS.Application.DTOs.Production.Recipes.Relations;
using MySaaS.Domain.Entities.Production;
using MySaaS.Tests.Factory;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace MySaaS.Tests.Tests
{
    [Collection("IntegrationTests")]
    public class IngredientControllerTests : BaseIntegrationTest
    {
        public IngredientControllerTests(WebApiFactory factory) : base(factory)
        {
        }


        [Fact]
        public async Task CreateIngredient_WhenCalledWithValidData_ReturnsCreated()
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

            CreateIngredientDTO chocolate = new CreateIngredientDTO()
            {
                Name = "Chocolate",
                Description = "This is a test ingredient",
                RecipeInfo = null
            };

            HttpResponseMessage chocolateResponse = await client.PostAsJsonAsync("/api/ingredient", chocolate);
            IngredientResponse? chocolateIngredientResponse = await chocolateResponse.Content.ReadFromJsonAsync<IngredientResponse>();
            int idIngredient = chocolateIngredientResponse!.Id;


            CreateIngredientDTO rellenoDeChocolate = new CreateIngredientDTO()
            {
                Name = "Relleno de chocolate",
                Description = "This is a test ingredient",
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


            //Act

            HttpResponseMessage rellenoResponse = await client.PostAsJsonAsync("/api/ingredient", rellenoDeChocolate);
            

            //Assert
            rellenoResponse.IsSuccessStatusCode.Should().BeTrue(); // No olvidarse

            IngredientResponse? rellenoIngredientResponse = await rellenoResponse.Content.ReadFromJsonAsync<IngredientResponse>();
            int idRellenoIngredient = rellenoIngredientResponse!.Id;
            HttpResponseMessage getRellenoResponse = await client.GetAsync($"/api/ingredient/{idRellenoIngredient}");
            IngredientDTO? getRellenoIngredientResponse = await getRellenoResponse.Content.ReadFromJsonAsync<IngredientDTO>();

            getRellenoIngredientResponse!.Id.Should().Be(idRellenoIngredient);
            getRellenoIngredientResponse.Name.Should().Be(rellenoDeChocolate.Name);
            getRellenoIngredientResponse.Description.Should().Be(rellenoDeChocolate.Description);
            getRellenoIngredientResponse.Recipe.Should().NotBeNull();
            getRellenoIngredientResponse.Recipe.Name.Should().Be(rellenoDeChocolate.Name);
            getRellenoIngredientResponse.Recipe.Description.Should().Be(rellenoDeChocolate.Description);
            getRellenoIngredientResponse.Recipe.Ingredients.Should().HaveCount(1);
            var component = getRellenoIngredientResponse.Recipe.Ingredients[0];
            component.Ingredient.Id.Should().Be(idIngredient);
            component.Ingredient.Name.Should().Be(chocolate.Name);
            component.Ingredient.Description.Should().Be(chocolate.Description);
            component.Weight.Amount.Should().Be(rellenoDeChocolate.RecipeInfo.Ingredients[0].Weight.Amount);
            component.Weight.Unit.Id.Should().Be(idUnit);
            component.Waste.Amount.Should().Be(rellenoDeChocolate.RecipeInfo.Ingredients[0].Waste.Amount);
            component.Waste.Unit.Id.Should().Be(idUnit);
            getRellenoIngredientResponse.Recipe.Quantity.Amount.Should().Be(rellenoDeChocolate.RecipeInfo.Quantity.Amount);
            getRellenoIngredientResponse.Recipe.Quantity.Unit.Id.Should().Be(idUnit);

        }

    }
}
