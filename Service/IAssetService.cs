using PortfolioVisualizer.Model;
using System.Threading.Tasks;

namespace PortfolioVisualizer.Service
{
    public interface IAssetService
    {
        Task<decimal> GetAssetPrice(Asset asset);
    }
}