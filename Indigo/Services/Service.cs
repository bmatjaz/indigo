using CsvHelper;
using Indigo.Models;
using System.Globalization;
using System.Net;

namespace Indigo.Services
{
    public class Service
    {

        public List<Region> getCsvData(string? name = null, string? dateFrom = null, string? dateTo = null)
        {
            List<Region> listOfRegions = new List<Region>();
            using (var client = new WebClient())
            {
                using (var stream = client.OpenRead("https://raw.githubusercontent.com/sledilnik/data/master/csv/region-cases.csv"))
                using (var reader = new StreamReader(stream))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<dynamic>();
                        foreach (var record in records)
                        {
                            string[] regions = new string[] { "LJ", "CE", "NM", "KR", "KP", "MB", "MS", "NG", "PO", "SG", "ZA" };
                            foreach (var regionName in regions)
                            {
                                Region region = new Region();
                                IDictionary<string, object> dict2 = record;
                                var c = dict2["date"].ToString();
                                var a = 1;
                                region.date = DateTime.Parse(dict2["date"].ToString());
                                region.regionName = regionName;
                                region.active = (dict2["region." + regionName.ToLower() + ".cases.active"]).ToString();
                                region.vaccinatedOnce = (dict2["region." + regionName.ToLower() + ".vaccinated.1st.todate"]).ToString();
                                region.vacinatedTwice = (dict2["region." + regionName.ToLower() + ".vaccinated.2nd.todate"]).ToString();
                                region.deceased = (dict2["region." + regionName.ToLower() + ".deceased.todate"].ToString());
                                region.confirmedToDate = (dict2["region." + regionName.ToLower() + ".cases.confirmed.todate"].ToString());
                                listOfRegions.Add(region);
                            }
                        }
                    }
                }
            }

            if (name != null)
                listOfRegions = listOfRegions.Where(region => region.regionName == name).ToList();

            if (dateFrom != null)
                listOfRegions = listOfRegions.Where(region => region.date >= DateTime.Parse(dateFrom)).ToList();

            if (dateTo != null)
                listOfRegions = listOfRegions.Where(region => region.date <= DateTime.Parse(dateTo)).ToList();

            return listOfRegions;
        }

        public List<RegionAverageDTO> getSevenDayAverage()
        {
            List<Region> regions = getCsvData();
            List<Region> listOfRegionsByAverage = new List<Region>();
            regions = regions.Where(region => region.date >= DateTime.Now.AddDays(-8)).ToList();
            var groupedRegions = regions.GroupBy(region => region.regionName);
            List<RegionAverageDTO> averageList = new List<RegionAverageDTO>();
            
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
            return averageList;
        }
    }
}
