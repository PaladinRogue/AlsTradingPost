using PaladinRogue.Libray.Core.Api.Pagination.Interfaces;

namespace PaladinRogue.Libray.Core.Api.Links
{
    public interface IPagingLinkBuilder
    {
        PageLink BuildLink(string name, string uri, IPaginationTemplate paginationTemplate);
        PagingLinks BuildPagingLinks(IPaginationTemplate paginationTemplate, ILink selfLink, int? totalResults);
    }
}
