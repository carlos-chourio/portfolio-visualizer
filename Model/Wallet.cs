using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioVisualizer.Model
{
    public class Wallet
    {
        public List<AssetBalance> Balance { get; set; } = new List<AssetBalance>();

        public decimal GetTotal() => Balance.Sum(t => t.Total);
    }

    public class AssetBalance
    {
        public AssetBalance(Asset asset, decimal amount, decimal currentPrice)
        {
            Asset = asset;
            Amount = amount;
            CurrentPrice = currentPrice;
        }

        public Asset Asset { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Total => Amount * CurrentPrice;
    }
}
