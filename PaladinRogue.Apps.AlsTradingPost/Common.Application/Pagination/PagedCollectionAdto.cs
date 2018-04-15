﻿using System.Collections.Generic;
using Common.Application.Pagination.Interfaces;

namespace Common.Application.Pagination
{
    public class PagedCollectionAdto<T> : IPagedCollectionAdto<T>
    {
        public int PageSize { get; set; }
        public int PageOffset { get; set; }
        public IList<T> Results { get; set; }
        public int TotalResults { get; set; }
    }
}
