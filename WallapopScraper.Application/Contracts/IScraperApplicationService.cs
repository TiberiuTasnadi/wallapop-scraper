using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallapopScraper.Application.DTOs;

namespace WallapopScraper.Application.Contracts
{
    public interface IScraperApplicationService
    {
        Task UploadData();
        Task<WorkerConfigurationDto> GetConfiguration(Guid configurationId);
        Task<WorkerConfigurationDto> CreateConfiguration(Guid configurationId);
    }
}
