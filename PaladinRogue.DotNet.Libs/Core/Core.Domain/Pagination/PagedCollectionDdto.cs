using System;
using System.Collections.Generic;
using PaladinRogue.Library.Core.Domain.Pagination.Interfaces;

namespace PaladinRogue.Library.Core.Domain.Pagination
{
    public class PagedCollectionDdto<T, TOut> : IPagedCollectionDdto<T>
    {
        protected PagedCollectionDdto(IList<T> results, int totalResults)
        {
            Results = results;
            TotalResults = totalResults;
        }

        public static TOut Create(IList<T> results, int totalResults)
        {
            return (TOut) Activator.CreateInstance(typeof(TOut), results, totalResults);
        }

        public IList<T> Results { get; set; }
        public int TotalResults { get; set; }
    }
}
