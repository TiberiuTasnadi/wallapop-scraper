using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WallapopScraper.Persistence.Model.Base
{
    public class BaseObject
    {
        [Key]
        public Guid Id { get; set; }
    }
}
