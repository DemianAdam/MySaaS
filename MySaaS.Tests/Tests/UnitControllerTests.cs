using MySaaS.Application.DTOs.Common.Unities;
using MySaaS.Tests.Factory;
using System.Net.Http.Json;
using FluentAssertions;

namespace MySaaS.Tests.Tests
{
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
            var responseResult = await response.Content.ReadFromJsonAsync<UnitDTO>();

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
        public async Task CreateUnit_WhenDeletingUnit_ReturnsSucess()
        {
            //arrange
            HttpClient client = Factory.CreateClient();
            CreateUnitDTO createUnitDTO = new CreateUnitDTO() { Name = "Test", };
            var response = await client.PostAsJsonAsync("/api/unit", createUnitDTO);

            //act

            var createdUnit = await response.Content.ReadFromJsonAsync<UnitDTO>();

            var response2 = await client.DeleteAsync("/api/unit/" + createdUnit!.Id);

            //assert
            response2.IsSuccessStatusCode.Should().BeTrue();
        }

    }
}
