using System.Net;
using System.Net.Http.Json;
using application.StockManager.Application.Dtos.resquests;
using FluentAssertions;

namespace StockManager.Tests;
public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task  Post_creates_product_and_returns_201()
    {
        CreateProductDto newProduct = new()
        {
            name = "teste Product",
            amount = 10,
            discount = 5,
            urlImage = "https://umaImagemFake.jpg",
            value = 100
        };

        var response = await _client.PostAsJsonAsync("/api/v1/product", newProduct);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
