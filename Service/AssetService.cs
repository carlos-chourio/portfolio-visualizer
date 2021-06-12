using Microsoft.EntityFrameworkCore;
using PortfolioVisualizer.Data;
using PortfolioVisualizer.Extensions;
using PortfolioVisualizer.Model;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PortfolioVisualizer.Service
{
    public interface IAssetService : IDataService<Model.Asset, string>
    {
    }

    public class AssetService : GenericDataService<Model.Asset, string, PortfolioDbContext>, IAssetService
    {
        public AssetService(PortfolioDbContext context) : base(context)
        {
        }

        public override PagedResult<Asset> GetPages(int pageNumber, int pageSize) => context.Set<Asset>().Include(t => t.Type).ToPage(pageNumber, pageSize);
        public override async Task<PagedResult<Asset>> GetPagesAsync(int pageNumber, int pageSize) => await context.Set<Asset>().Include(t => t.Type).ToPageAsync(pageNumber, pageSize);
        public override IQueryable<Asset> GetBy(Expression<Func<Asset, bool>> expression) => context.Set<Asset>().Include(t=> t.Type).AsNoTracking().Where(expression);
    }

    //public class AssetPrice
    //{
    //    public string Symbol { get; set; }
    //    public decimal Price { get; set; }
    //}

    //public class AssetService 
    //{
    //    private readonly HttpClient httpClient;
    //    private readonly PortfolioDbContext dbContext;

    //    public AssetService(HttpClient httpClient, Data.PortfolioDbContext dbContext)
    //    {
    //        this.httpClient = httpClient;
    //        this.dbContext = dbContext;
    //    }

    //    public ValueTask<Model.Asset> GetAssetAsync(string symbol) => dbContext.Assets.FindAsync(symbol);

    //    public IQueryable<Model.Asset> GetAssetBy(Model.AssetType type) => dbContext.Assets.Where(t => t.Type.Equals(type));

    //    public async Task<decimal> GetAssetPrice(Model.Asset asset)
    //    {
    //        var response = await httpClient.GetAsync($"https://api.binance.com/api/v3/ticker/price?symbol={asset.Symbol}USDT");
    //        if (response.IsSuccessStatusCode) {
    //            var jsonResponse = await response.Content.ReadAsStringAsync();
    //            var assetPrice = JsonConvert.DeserializeObject<AssetPrice>(jsonResponse);
    //            return assetPrice.Price;
    //        }
    //        return default;
    //    }
    //}
}
