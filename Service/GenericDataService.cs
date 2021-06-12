using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using PortfolioVisualizer.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PortfolioVisualizer.Extensions;

namespace PortfolioVisualizer.Service {
    public interface IDataService<TModel, TId> where TModel : class {
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
        where TModel : class {

        protected readonly TContext context;

        public GenericDataService(TContext context) {
            this.context = context;
        }

        public virtual void Add(TModel model) => context.Set<TModel>().Add(model);
        public virtual void Delete(TModel model) => context.Set<TModel>().Remove(model);

        public virtual TModel Find(TId id) {
            var entity = context.Set<TModel>().Find(id);
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async ValueTask<TModel> FindAsync(TId id) {
            var entity = await context.Set<TModel>().FindAsync(id);
            if (entity != null) {
                context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public virtual IQueryable<TModel> Get() => context.Set<TModel>();
        public virtual Task<List<TModel>> GetAsync() => Get().ToListAsync();
        public virtual IQueryable<TModel> GetBy(Expression<Func<TModel, bool>> expression) => context.Set<TModel>().Where(expression);
        public virtual Task<List<TModel>> GetByAsync(Expression<Func<TModel, bool>> expression) => GetBy(expression).ToListAsync();
        public virtual PagedResult<TModel> GetPages(int pageNumber, int pageSize) => context.Set<TModel>().ToPage(pageNumber, pageSize);
        public virtual async Task<PagedResult<TModel>> GetPagesAsync(Expression<Func<TModel, bool>> expression, int pageNumber, int pageSize) => await context.Set<TModel>().Where(expression).ToPageAsync(pageNumber, pageSize);
        public virtual async Task<PagedResult<TModel>> GetPagesAsync(int pageNumber, int pageSize) => await context.Set<TModel>().ToPageAsync(pageNumber, pageSize);
        public virtual int SaveChanges() => context.SaveChanges();
        public virtual Task<int> SaveChangesAsync() => context.SaveChangesAsync();
        public virtual void Update(TModel model) => context.Update(model);
    }
}
