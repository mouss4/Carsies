using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.IntegrationTest.Fixtures;
using AuctionService.IntegrationTests.Util;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.IntegrationTest
{
    public class AuctionControllerTests : IClassFixture<CustomWebAppFactory>, IAsyncLifetime
    {
        private readonly CustomWebAppFactory _factory;
        private readonly HttpClient _httpClient;

        public AuctionControllerTests(CustomWebAppFactory factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }
        [Fact]
        public async Task GetAuctions_ShouldReturn3Auctions()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetFromJsonAsync<List<AuctionDto>>("api/auctions");

            // Assert
            Assert.Equal(3, response.Count);
            
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public Task DisposeAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();

            DbHelper.ReinitDbForTests(db);
            return Task.CompletedTask;
        }
    }
}
