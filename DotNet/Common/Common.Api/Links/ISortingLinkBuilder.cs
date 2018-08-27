using Common.Api.Sorting;

namespace Common.Api.Links
{
    public interface ISortingLinkBuilder
    {
        SortLink BuildLink(string name, string uri, ISortTemplate sortTemplate);
    }
}
