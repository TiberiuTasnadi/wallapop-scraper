using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using WallapopScraper.Application.Applications;
using WallapopScraper.Application.Contracts;
using WallapopScraper.WindowsService;
using WallapopScrapper.Service.Service;

namespace WallapopScrapper.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = ".NET Wallapop Service";
            });

            builder.Services.AddTransient<IScraperService, ScraperService>();
            builder.Services.AddTransient<IScraperApplicationService, ScraperApplicationService>();

            builder.Services.AddHostedService<Worker>();

            builder.Services.Configure<ConfigurationSettings>(builder.Configuration.GetSection("ConfigurationSettings"));

            var host = builder.Build();
            host.Run();
        }
    }
}