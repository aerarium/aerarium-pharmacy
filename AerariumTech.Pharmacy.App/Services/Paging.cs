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
    public class PaginatedCollection<TEntity> : List<TEntity>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        protected PaginatedCollection(IEnumerable<TEntity> entities, int count, int pageIndex, int perPage)
            : base(entities)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) perPage);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedCollection<TEntity>> CreateAsync(IQueryable<TEntity> source, int pageIndex,
            int perPage)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedCollection<TEntity>(items, count, pageIndex, perPage);
        }
    }
}