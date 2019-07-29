using System;
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
        public string Code { get; protected set; }

        [Required]
        public virtual ReferenceDataType ReferenceDataType { get; protected set; }

        public Guid ReferenceDataTypeId { get; protected set; }

        public IAggregateRoot AggregateRoot => ReferenceDataType;
    }
}