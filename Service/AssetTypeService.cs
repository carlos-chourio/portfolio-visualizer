using System;
using PortfolioVisualizer.Data;

namespace PortfolioVisualizer.Service
{
    public interface IAssetTypeService : IDataService<Model.AssetType, Guid>
    {
    }

    public class AssetTypeService : GenericDataService<Model.AssetType, Guid, PortfolioDbContext>, IAssetTypeService
    {
        public AssetTypeService(PortfolioDbContext context) : base(context)
        {
        }
    }
}
