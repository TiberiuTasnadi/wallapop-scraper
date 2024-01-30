using Microsoft.Extensions.Logging;
using WallapopScrapper.Service.Service;

namespace WallapopScrapper.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IScraperService _scraperService;
        public Worker(ILogger<Worker> logger, IScraperService scraperService)
        {
            _logger = logger;
            _scraperService = scraperService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _scraperService.Execute();
                _logger.LogWarning("Process completed...");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
