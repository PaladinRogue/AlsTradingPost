﻿namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Settings
{
    public class MessagingBusSettings
    {
        public int? RetryCount { get; set; }

        public string Connection { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
