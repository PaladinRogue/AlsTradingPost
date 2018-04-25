using System.Collections.Generic;

namespace Common.Api.Links
{
    public interface ILinkBuilder
    {
        IList<Link> BuildLinks<T>(T data);
    }
}