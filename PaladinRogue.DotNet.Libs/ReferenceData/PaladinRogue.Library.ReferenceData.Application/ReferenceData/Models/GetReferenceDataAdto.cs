using System;

namespace PaladinRogue.Library.ReferenceData.Application.ReferenceData.Models
{
    public class GetReferenceDataAdto
    {
        public Guid? Id { get; set; }

        public string Type { get; set; }

        public string Code { get; set; }
    }
}