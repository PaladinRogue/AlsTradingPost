using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AlsTradingPost.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
