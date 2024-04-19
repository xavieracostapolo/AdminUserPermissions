using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;

namespace Xacosta.AdminPermisos.IntegrationTests
{
    public class TestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TestBase(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetApiPermissions_Returns_SuccessAndCorrectContentType()
        {
            //Arrange
            var client = _factory.CreateClient();
            //Act
            var response = await client.GetAsync("api/Permissions");
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task RequestApiPermissions_Returns_SuccessAndCorrectContentType()
        {
            //Arrange
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    nombreEmpleado = "nombreEmpleado Unit Test",
                    apellidoEmpleado = "apellidoEmpleado Unit Test",
                    tipoPermiso = "tipoPermiso Unit Test"
                }),
                Encoding.UTF8,
                "application/json");

            var client = _factory.CreateClient();
            //Act
            var response = await client.PostAsync("api/Permissions", jsonContent);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task ModifyApiPermissions_Returns_SuccessAndCorrectContentType()
        {
            //Arrange
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    idPermiso = 15,
                    nombreEmpleado = "nombreEmpleado Unit Test",
                    apellidoEmpleado = "apellidoEmpleado Unit Test",
                    tipoPermiso = 10
                }),
                Encoding.UTF8,
                "application/json");

            var client = _factory.CreateClient();
            //Act
            var response = await client.PutAsync("api/Permissions", jsonContent);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
