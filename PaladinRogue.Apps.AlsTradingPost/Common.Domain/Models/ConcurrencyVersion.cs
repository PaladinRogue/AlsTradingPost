using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Models
{
    public class ConcurrencyVersion : IConcurrencyVersion
    {

        public ConcurrencyVersion(IEntity entity)
        {
            Version = entity.Version;
        }

        public byte[] Version { get; set; }
    }
}
