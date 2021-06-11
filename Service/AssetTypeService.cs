using PortfolioVisualizer.Data;
using System;

namespace PortfolioVisualizer.Service
{
    public interface IAssetTypeService : IDataService<Model.AssetType, Guid>
    {
    }

    public class AssetTypeService : GenericDataService<Model.AssetType, Guid, Data.PortfolioDbContext>, IAssetTypeService
    {
        public AssetTypeService(PortfolioDbContext context) : base(context)
        {
        }
    }
}
