using System.ComponentModel.DataAnnotations;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Resources;

namespace ReferenceData.Domain
{
    public class ReferenceDataValue : Entity, IAggregateMember
    {
        protected ReferenceDataValue()
        {
        }

        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Code { get; set; }

        [Required]
        public virtual ReferenceDataType ReferenceDataType { get; set; }

        public IAggregateRoot AggregateRoot => ReferenceDataType;
    }
}