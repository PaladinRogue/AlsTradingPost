﻿namespace Common.Application.Pagination.Interfaces
{
    public interface IPaginationAdto
    {
        int PageSize { get; set; }
        int PageOffset { get; set; }
    }
}
