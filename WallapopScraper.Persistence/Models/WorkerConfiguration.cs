using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallapopScraper.Persistence.Model.Base;

namespace WallapopScraper.Persistence.Model
{
    public class WorkerConfiguration : BaseObject
    {
        public string Url { get; set; } = string.Empty;
        public string CardClass { get; set; } = string.Empty;
        public string TitleClass { get; set; } = string.Empty;
        public string PriceClass { get; set; } = string.Empty;
        public string DataClass { get; set; } = string.Empty;
    }
}
