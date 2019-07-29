using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Resources;

namespace ReferenceData.Domain
{
    public class ReferenceDataType : VersionedEntity, IAggregateRoot
    {
        private readonly ISet<ReferenceDataValue> _referenceDataValues = new HashSet<ReferenceDataValue>();

        protected ReferenceDataType()
        {
        }

        public virtual IEnumerable<ReferenceDataValue> ReferenceDataValues => _referenceDataValues;

        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Type { get; protected set; }
    }
}