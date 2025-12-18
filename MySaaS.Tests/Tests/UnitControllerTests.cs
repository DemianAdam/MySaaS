using FluentAssertions;
using MySaaS.Application.DTOs.Common.Unit;
using MySaaS.Application.DTOs.Common.Unities;
using MySaaS.Application.DTOs.Production.Recipes;
using MySaaS.Application.DTOs.Production.Recipes.Components;
using MySaaS.Tests.Factory;
using System.Linq;
using System.Net;
using System.Net.Http.Json;

namespace MySaaS.Tests.Tests
{
    [Collection("IntegrationTests")]
    public class UnitControllerTests : BaseIntegrationTest
    {
        public UnitControllerTests(WebApiFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task CreateUnit_WhenCalledWithValidData_ReturnsCreated()
        {
            //Arrange
            HttpClient client = Factory.CreateClient();
            CreateUnitDTO createUnitDTO = new CreateUnitDTO()
            {
                Name = "Test",
            };

            //Act
            var response = await client.PostAsJsonAsync("/api/unit", createUnitDTO);

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            UnitResponse? responseResult = await response.Content.ReadFromJsonAsync<UnitResponse>();


            responseResult.Should().NotBeNull();
            responseResult.Id.Should().Be(1);
            responseResult.Name.Should().Be("Test");
        }

        [Fact]
        public async Task CreateUnit_WhenInsertingUnitsWithSameName_ReturnsError() 
        {
            //arrange
            HttpClient client = Factory.CreateClient();
            CreateUnitDTO createUnitDTO = new CreateUnitDTO()
            {
                Name = "Test",
            };

            CreateUnitDTO createUnitDTO2 = new CreateUnitDTO()
            {
                Name = "Test",
            };


            //act
            var response1 = await client.PostAsJsonAsync("/api/unit", createUnitDTO);
            var response2 = await client.PostAsJsonAsync("/api/unit", createUnitDTO2);

            //assert
            response1.IsSuccessStatusCode.Should().BeTrue();
            response2.IsSuccessStatusCode.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteEndPoint_WhenDeletingUnit_ReturnsSucess()
        {
            //arrange
            HttpClient client = Factory.CreateClient();
            CreateUnitDTO createUnitDTO = new CreateUnitDTO() { Name = "Test", };
            var response = await client.PostAsJsonAsync("/api/unit", createUnitDTO);

            //act

            var createdUnit = await response.Content.ReadFromJsonAsync<UnitResponse>();

            var response2 = await client.DeleteAsync("/api/unit/" + createdUnit!.Id);

            //assert
            response2.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteEndPoint_WhenDeletingUnitThatDoesNotExists_ReturnsNotFound()
        {
            //arrange
            HttpClient client = Factory.CreateClient();
            

            //act

            var response = await client.DeleteAsync("/api/unit/999");

            //assert

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task CreateEndPoint_WhenCreatingUnitWithoutName_ReturnsBadRequest()
        {
            //arrange
            HttpClient client = Factory.CreateClient();
            CreateUnitDTO createUnitDTO = new CreateUnitDTO()
            {
                Name = string.Empty,
            };

            //act
            var response = await client.PostAsJsonAsync("/api/unit", createUnitDTO);

            //assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
