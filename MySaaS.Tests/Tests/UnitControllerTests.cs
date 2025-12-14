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

    }
}
