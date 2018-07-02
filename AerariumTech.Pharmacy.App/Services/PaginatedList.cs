using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Helper to paginate large amounts of data,
    /// based on example by Microsoft in its docs.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PaginatedList<TEntity> : List<TEntity>
    {
        public int PageIndex { get; }
        public int TotalPages { get; }

        protected PaginatedList(IEnumerable<TEntity> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);

            AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<TEntity>> CreateAsync(IQueryable<TEntity> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<TEntity>(items, count, pageIndex, pageSize);
        }
    }
}