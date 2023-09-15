using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Shared;

namespace TaskManager.Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedList<T>> GetPagedItems<T>(this IQueryable<T> query, RequestParameter parameters, Expression<Func<T, bool>> searchExpression = null)
        {
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            if (searchExpression != null)
                query = query.Where(searchExpression);

            var items = await query.Skip(skip).Take(parameters.PageSize).ToListAsync();
            return new PagedList<T>(items, await query.CountAsync(), parameters.PageNumber, parameters.PageSize);
        }
    }
}
