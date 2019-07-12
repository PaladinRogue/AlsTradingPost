using System;

namespace ReverseProxy.ApplicationServices.Applications
{
    public class ApplicationAdto
    {
        public string ApplicationName { get; set; }

        public Uri HostUri { get; set; }
    }
}