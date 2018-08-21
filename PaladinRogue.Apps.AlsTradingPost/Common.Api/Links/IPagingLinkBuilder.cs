using Common.Api.Pagination.Interfaces;

namespace Common.Api.Links
{
    public interface IPagingLinkBuilder
    {
        PageLink BuildLink(string name, string uri, IPaginationTemplate paginationTemplate);
        PagingLinks BuildPagingLinks(IPaginationTemplate paginationTemplate, ILink selfLink, int? totalResults);
    }
}
