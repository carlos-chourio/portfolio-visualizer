using System;
using System.Linq;
using System.Threading.Tasks;
using PortfolioVisualizer.Model;
using Microsoft.EntityFrameworkCore;

namespace PortfolioVisualizer.Extensions
{
    public static class QueryableExtensions
    {
        public static PagedResult<T> ToPage<T>(this IQueryable<T> query, int pageNumber, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (pageNumber - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public static async Task<PagedResult<T>> ToPageAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = await query.CountAsync()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}
