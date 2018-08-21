using System;

namespace Common.Setup.Infrastructure.Constants
{
    [Flags]
    public enum HttpVerb
    {
        None,
        Get = 1,
        Put = 2,
        Post = 4,
        Delete = 8
    }
}
