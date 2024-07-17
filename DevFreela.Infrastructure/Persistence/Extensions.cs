using DevFreela.Core.Persistence.model;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence;

public static class Extensions
{
    
    public static async Task<PaginationResult<T>> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
    {

        var itemCount = await query.CountAsync();
        var pageCount = (double)itemCount / pageSize;
        var totalPages = (int)Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;

        var data = await query.Skip(skip).Take(pageSize).ToListAsync();

        return new(page, totalPages, pageSize, itemCount, data);
    }

}