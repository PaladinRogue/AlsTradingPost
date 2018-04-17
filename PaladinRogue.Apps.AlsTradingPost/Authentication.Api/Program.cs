using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Authentication.Api
{
    public class Program
    {
	    public static void Main(string[] args)
	    {
		    BuildWebHost(args)
                .Run();
	    }

	    public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
	            .UseUrls("http://localhost:1002")
                .UseStartup<Startup>()
	            .UseKestrel()
	            .UseIISIntegration()
                .Build();
    }
}
