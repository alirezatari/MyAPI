using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MyApi.Data;
using MyApi.Models;
using MyApi.Models.DTOs;
using MyApi.Tests.Helpers;
using Xunit;

namespace MyApi.Tests.Controllers
{
    public class ProductControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public ProductControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        private async Task<string> GetAdminTokenAsync()
        {
            var loginRequest = new LoginRequest { Username = "admin", Password = "Pass@123" };
            var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);
            response.EnsureSuccessStatusCode();
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return loginResponse!.Token;
        }

        [Fact]
        public async Task Delete_ProductThatExists_ReturnsNoContent()
        {
            // Arrange
            var token = await GetAdminTokenAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Seed a product to delete
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var product = new Product { Name = "Test Product to Delete", Price = 10 };
            context.Products.Add(product);
            await context.SaveChangesAsync();

            // Act
            var response = await _client.DeleteAsync($"/api/product/{product.Id}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ProductThatDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var token = await GetAdminTokenAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var nonExistentId = 999;

            // Act
            var response = await _client.DeleteAsync($"/api/product/{nonExistentId}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
