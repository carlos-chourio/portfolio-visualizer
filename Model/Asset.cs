using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioVisualizer.Model
{
    /// <summary>
    /// Model of the Asset (BTC - Crypto, TSLA - Stock, ...)
    /// </summary>
    public class Asset
    {
        [Key]
        public string Symbol { get; set; }
        public Guid TypeId { get; set; }
        public AssetType Type { get; set; }
    }

    /// <summary>
    /// Type of the asset (CRYPTO, STOCK, ETF, ...)
    /// </summary>
    public class AssetType
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static implicit operator Guid(AssetType assetType) => assetType.Id;
        public static implicit operator string(AssetType assetType) => assetType.Name;
    }
}
