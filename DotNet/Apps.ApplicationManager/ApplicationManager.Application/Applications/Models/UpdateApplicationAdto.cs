using System;

namespace ApplicationManager.ApplicationServices.Applications.Models
{
    public class UpdateApplicationAdto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SystemName { get; set; }
    }
}