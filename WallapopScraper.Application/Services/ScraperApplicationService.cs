using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallapopScraper.Application.Contracts;
using WallapopScraper.Application.DTOs;

namespace WallapopScraper.Application.Applications
{
    public class ScraperApplicationService : IScraperApplicationService
    {
        public Task<WorkerConfigurationDto> GetConfiguration(Guid configurationId)
        {
            throw new NotImplementedException();
        }

        public Task<WorkerConfigurationDto> CreateConfiguration(Guid configurationId)
        {
            throw new NotImplementedException();
        }

        public Task UploadData()
        {
            throw new NotImplementedException();
        }
    }
}
