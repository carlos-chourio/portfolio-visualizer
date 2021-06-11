using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PortfolioVisualizer.Data
{
    public class PortfolioDbContext : DbContext
    {
        public DbSet<Model.Asset> Assets { get; set; }
        public DbSet<Model.AssetType> AssetTypes { get; set; }

        public PortfolioDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var cryptoCurrency = new Model.AssetType { Id = Guid.Parse("{E23C8752-A2FA-4CBF-801F-F2B7D45C0C52}"), Name = "Crypto" };
            var stock = new Model.AssetType { Id = Guid.Parse("{354F8FE2-FFB3-46DE-8EFD-3CB02C01B9D1}"), Name = "Stock" };
            var etf = new Model.AssetType { Id = Guid.Parse("{20047E17-CA21-4487-81E0-8FBBABF116DA}"), Name = "ETF" };

            modelBuilder.Entity<Model.AssetType>().HasData(cryptoCurrency, stock, etf);
            modelBuilder.Entity<Model.Asset>().HasData(
                new Model.Asset
                {
                    Symbol = "BTC",
                    TypeId = cryptoCurrency
                },
                new Model.Asset
                {
                    Symbol = "ADA",
                    TypeId = cryptoCurrency
                }
                , new Model.Asset
                {
                    Symbol = "ETH",
                    TypeId = cryptoCurrency
                });
        }
    }

    public static class DbSetExtensions
    {
        public static T AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return !exists ? dbSet.Add(entity).Entity : null;
        }
    }
}
