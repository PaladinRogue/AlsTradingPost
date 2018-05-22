using Authentication.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Persistence.EntityFramework.Infrastructure.Extensions;

namespace Authentication.Api
{
    public class Program
    {
	    public static void Main(string[] args)
	    {
		    CreateWebHostBuilder(args)
			    .Build()
			    .ApplyMigrations<AuthenticationDbContext>()
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
