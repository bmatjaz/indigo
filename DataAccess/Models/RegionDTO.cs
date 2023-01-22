using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class RegionDTO
    {
        public DateTime date { get; set; }

        public string regionName { get; set; }

        public string active { get; set; }

        public string vaccinatedOnce { get; set; }

        public string vacinatedTwice { get; set; }

        public string deceased { get; set; }
    }
}
