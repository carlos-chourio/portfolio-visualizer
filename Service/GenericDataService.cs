using Microsoft.EntityFrameworkCore;
using PortfolioVisualizer.Extensions;
using PortfolioVisualizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioVisualizer.Service
{
    public interface IDataService<TModel, TId> where TModel : class
    {
        void Add(TModel model);
        TModel Find(TId id);
        ValueTask<TModel> FindAsync(TId id);
        IQueryable<TModel> Get();
        Task<List<TModel>> GetAsync();

        Task<PagedResult<TModel>> GetPagesAsync(Expression<Func<TModel, bool>> expression, int pageNumber, int pageSize);
        Task<PagedResult<TModel>> GetPagesAsync(int pageNumber, int pageSize);
        PagedResult<TModel> GetPages(int pageNumber, int pageSize);

        IQueryable<TModel> GetBy(Expression<Func<TModel, bool>> expression);
        Task<List<TModel>> GetByAsync(Expression<Func<TModel, bool>> expression);
        void Delete(TModel model);
        void Update(TModel model);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }

    public abstract class GenericDataService<TModel, TId, TContext> : IDataService<TModel, TId> 
        where TContext : DbContext
        where TModel : class
    {
        private readonly TContext context;

        public GenericDataService(TContext context)
        {
            this.context = context;
        }

        public void Add(TModel model) => context.Set<TModel>().Add(model);
        public void Delete(TModel model) => context.Set<TModel>().Remove(model);
        public TModel Find(TId id) => context.Set<TModel>().Find(id);
        public ValueTask<TModel> FindAsync(TId id) => context.Set<TModel>().FindAsync(id);
        public IQueryable<TModel> Get() => context.Set<TModel>();
        public Task<List<TModel>> GetAsync() => Get().ToListAsync();
        public IQueryable<TModel> GetBy(Expression<Func<TModel, bool>> expression) => context.Set<TModel>().Where(expression);
        public Task<List<TModel>> GetByAsync(Expression<Func<TModel, bool>> expression) => GetBy(expression).ToListAsync();
        public PagedResult<TModel> GetPages(int pageNumber, int pageSize) => context.Set<TModel>().ToPage(pageNumber, pageSize);
        public async Task<PagedResult<TModel>> GetPagesAsync(Expression<Func<TModel, bool>> expression, int pageNumber, int pageSize) => await context.Set<TModel>().Where(expression).ToPageAsync(pageNumber, pageSize);
        public async Task<PagedResult<TModel>> GetPagesAsync(int pageNumber, int pageSize) => await context.Set<TModel>().ToPageAsync(pageNumber, pageSize);
        public int SaveChanges() => context.SaveChanges();
        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
        public void Update(TModel model) => context.Set<TModel>().Update(model);
    }
}
