using Authentication.Setup.Infrastructure.DbInitializer;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Authentication.Api
{
    public class Program
    {
	    public static void Main(string[] args)
	    {
		    CreateWebHostBuilder(args)
			    .Build()
			    .ApplyMigrations()
                .Run();
	    }

	    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
	            .UseUrls("https://localhost:1002")
                .UseStartup<Startup>()
	            .UseKestrel()
	            .UseIISIntegration();
    }
}
