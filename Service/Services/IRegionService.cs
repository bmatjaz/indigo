using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Service.Services
{
    public interface IRegionService
    {
        public List<RegionAverageDTO> getSevenDayAverage();

        public List<RegionDTO> getAllCases(string? name = null, string? dateFrom = null, string? dateTo = null);
    }
}
