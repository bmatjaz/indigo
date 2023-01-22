using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccess;
using DataAccess.Models;
using DataAccess.Repository;

namespace Service.Services
{
    public class RegionService : IRegionService
    {
        private IRegionRepository _regionRepository;

        public RegionService()
        {
            _regionRepository = new RegionRepository();
        }

        public List<RegionDTO> getAllCases(string? name = null, string? dateFrom = null, string? dateTo = null)
        {
            List<RegionDTO> listOfRegions = _regionRepository.getCsvForAll();

            if (name != null)
                listOfRegions = listOfRegions.Where(region => region.regionName == name.ToUpper()).ToList();

            if (dateFrom != null)
                listOfRegions = listOfRegions.Where(region => region.date >= DateTime.Parse(dateFrom)).ToList();

            if (dateTo != null)
                listOfRegions = listOfRegions.Where(region => region.date <= DateTime.Parse(dateTo)).ToList();

            return listOfRegions;
        }

        public List<RegionAverageDTO> getSevenDayAverage()
        {
            List<Region> listOfRegions = _regionRepository.getCsvForLastWeek();

            listOfRegions = listOfRegions.Where(region => region.date >= DateTime.Now.AddDays(-8)).ToList();
            var groupedRegions = listOfRegions.GroupBy(region => region.regionName);
            List<RegionAverageDTO> averageList = new List<RegionAverageDTO>();

            // get all grouped regions.
            // Loop trough each region.
            // We have 8 days so we can get the difference for day 7 from today because day7 - day8 = new cases in day7
            // if confirmedUntilYesterday value is -1 we know that its day 8 and we just set the value there
            foreach (var group in groupedRegions)
            {
                RegionAverageDTO regionDTO = new RegionAverageDTO();
                int confirmedUntilYesterday = -1;
                int numberOfCasesLastSevenDays = 0;
                int numberOfReturnedDays = group.Count() - 1;

                foreach (var region in group)
                {
                    if (confirmedUntilYesterday == -1)
                    {
                        confirmedUntilYesterday = int.Parse(region.confirmedToDate);
                        continue;
                    }
                    else
                    {
                        numberOfCasesLastSevenDays += int.Parse(region.confirmedToDate) - confirmedUntilYesterday;
                        confirmedUntilYesterday = int.Parse(region.confirmedToDate);
                        regionDTO.name = region.regionName;
                    }
                }
                regionDTO.average = numberOfCasesLastSevenDays / numberOfReturnedDays;
                averageList.Add(regionDTO);
            }
            averageList = averageList.OrderByDescending(region => region.average).ToList();

            return averageList;
        }
    }
}
