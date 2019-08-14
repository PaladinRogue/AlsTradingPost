using System;

namespace PaladinRogue.Library.Core.Api.Links
{
    [Serializable]
    public class SelfLinkNotSetException : Exception
    {
        public SelfLinkNotSetException() : this(string.Empty)
        {
        }

        public SelfLinkNotSetException(string message) : this(message, null)
        {
        }

        public SelfLinkNotSetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
