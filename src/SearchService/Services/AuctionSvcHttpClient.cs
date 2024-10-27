using MongoDB.Entities;
using SearchService.Models;
using System.Net.Http;

namespace SearchService.Services
{
    public class AuctionSvcHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AuctionSvcHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<Item>> GetItemsForSearchDb()
        {
            var lastUpdated = await DB.Find<Item, string>()
                .Sort(x => x.Descending(x => x.UpdatedAt))
                .Project(x => x.UpdatedAt.ToString())
                .ExecuteFirstAsync();


            var auctionURL = _config["AuctionServiceUrl"]
                ?? throw new ArgumentNullException("Cannot get auction address");

            var url = auctionURL + "/api/auctions";

            if (!string.IsNullOrEmpty(lastUpdated))
            {
                url += $"?date={lastUpdated}";
            }

            var items = await _httpClient.GetFromJsonAsync<List<Item>>(url);

            return items ?? [];

        }
    }
}
