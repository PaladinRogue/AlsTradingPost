using PaladinRogue.Library.Core.Api.Sorting;

namespace PaladinRogue.Library.Core.Api.Links
{
    public interface ISortingLinkBuilder
    {
        SortLink BuildLink(string name, string uri, ISortTemplate sortTemplate);
    }
}
