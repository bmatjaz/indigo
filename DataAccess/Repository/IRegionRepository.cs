using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public interface IRegionRepository
    {
        public List<Region> getCsvForLastWeek();
        public List<RegionDTO> getCsvForAll();
    }
}
