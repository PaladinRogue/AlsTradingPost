using System;

namespace ApplicationManager.Domain.Applications.Create
{
    public class CreateApplicationDdto
    {
        public string Name { get; set; }

        public string SystemName { get; set; }

        public string HostUri { get; set; }
    }
}