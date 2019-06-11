using System.Collections.Generic;

namespace Common.Setup.Settings
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string AuthenticationUrl { get; set; }

        public string AuthenticationSecret { get; set; }

        public Dictionary<string, string> ConnectionStrings { get; set; }

        public string Name { get; set; }

        public string SystemName { get; set; }
    }
}
