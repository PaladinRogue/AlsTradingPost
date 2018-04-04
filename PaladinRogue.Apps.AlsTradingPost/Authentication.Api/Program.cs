using Common.Resources.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Authentication.Api
{
    public class Program
    {
	    public static void Main(string[] args)
	    {
		    BuildWebHost(args)
                .CheckForApplications()
                .Run();
	    }

	    public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
				.UseIISIntegration()
                .Build();
    }
}
