using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
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
            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}