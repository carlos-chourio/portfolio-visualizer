using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PortfolioVisualizer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioVisualizer.Service
{
    public class AssetPrice
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
    }

    public class AssetService : IAssetService
    {
        private readonly HttpClient httpClient;
        private readonly PortfolioDbContext dbContext;

        public AssetService(HttpClient httpClient, Data.PortfolioDbContext dbContext)
        {
            this.httpClient = httpClient;
            this.dbContext = dbContext;
        }

        public ValueTask<Model.Asset> GetAssetAsync(string symbol) => dbContext.Assets.FindAsync(symbol);

        public IQueryable<Model.Asset> GetAssetBy(Model.AssetType type) => dbContext.Assets.Where(t => t.Type.Equals(type));

        public async Task<decimal> GetAssetPrice(Model.Asset asset)
        {
            var response = await httpClient.GetAsync($"https://api.binance.com/api/v3/ticker/price?symbol={asset.Symbol}USDT");
            if (response.IsSuccessStatusCode) {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var assetPrice = JsonConvert.DeserializeObject<AssetPrice>(jsonResponse);
                return assetPrice.Price;
            }
            return default;
        }
    }
}
