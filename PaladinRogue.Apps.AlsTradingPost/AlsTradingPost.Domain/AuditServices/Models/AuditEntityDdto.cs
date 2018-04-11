using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.AuditServices.Models
{
    public class AuditEntityDdto
    {
        public IEntity Entity { get; set; }
    }
}
