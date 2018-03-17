using Common.Api.Interfaces;

namespace Common.Api.Request
{
    public class ConcurrencyVersion : IConcurrencyVersion
    {
        public byte[] Version { get; set; }
    }
}
