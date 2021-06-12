using System.ComponentModel.DataAnnotations;

namespace PortfolioVisualizer.DTO.Out
{
    public class Asset
    {
        [Required(ErrorMessageResourceType = typeof(Properties.Resource), ErrorMessageResourceName = nameof(Properties.Resource.Symbol))]
        public string Symbol { get; set; }

        [Required(ErrorMessageResourceType = typeof(Properties.Resource), ErrorMessageResourceName = nameof(Properties.Resource.Type))]
        public AssetType Type { get; set; }
    }
}
