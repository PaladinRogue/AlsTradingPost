using System;
using System.ComponentModel.DataAnnotations;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Domain.Aggregates;
using PaladinRogue.Library.Core.Domain.Entities;

namespace PaladinRogue.Library.ReferenceData.Domain
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