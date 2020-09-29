using HomeTask4.Cmd.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeTask4.Cmd
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
            //// inject a BackgroundService
            services.AddHostedService<StartAppService>();

            // add in an IHttpClientFactory
            services.AddHttpClient();

			// add in IMemoryCache
			services.AddMemoryCache();
		}
	}
}
