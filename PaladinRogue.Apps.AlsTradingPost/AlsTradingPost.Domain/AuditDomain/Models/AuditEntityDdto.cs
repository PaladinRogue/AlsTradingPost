using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.AuditDomain.Models
{
    public class AuditEntityDdto
    {
        public IEntity Entity { get; set; }
    }
}
