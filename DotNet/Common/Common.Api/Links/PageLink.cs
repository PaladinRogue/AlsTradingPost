﻿namespace Common.Api.Links
{
    public class PageLink : SortLink
    {
        public int PageSize { get; set; }

        public int PageOffset { get; set; }
    }
}