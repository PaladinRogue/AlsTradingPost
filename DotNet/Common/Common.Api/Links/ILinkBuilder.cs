using Common.Api.Resources;

namespace Common.Api.Links
{
    public interface ILinkBuilder
    {
        Links BuildLinks(IResource resource, ITemplate template = null);
    }
}