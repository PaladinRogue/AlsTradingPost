using PaladinRogue.Libray.Core.Api.Sorting;

namespace PaladinRogue.Libray.Core.Api.Links
{
    public interface ISortingLinkBuilder
    {
        SortLink BuildLink(string name, string uri, ISortTemplate sortTemplate);
    }
}
