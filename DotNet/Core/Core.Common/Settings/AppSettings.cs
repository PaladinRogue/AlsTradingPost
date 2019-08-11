﻿using System.Collections.Generic;

namespace PaladinRogue.Libray.Core.Common.Settings
{
    public class AppSettings
    {
        public Dictionary<string, string> ConnectionStrings { get; set; }

        public string Name { get; set; }

        public string SystemName { get; set; }
    }
}
