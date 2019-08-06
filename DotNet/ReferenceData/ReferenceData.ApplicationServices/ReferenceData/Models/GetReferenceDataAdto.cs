using System;

namespace ReferenceData.ApplicationServices.ReferenceData.Models
{
    public class GetReferenceDataAdto
    {
        public Guid? Id { get; set; }

        public string Type { get; set; }

        public string Code { get; set; }
    }
}