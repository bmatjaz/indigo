using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using DataAccess.Models;


namespace DataAccess.Repository
{
    public class RegionRepository : IRegionRepository
    {
        public List<Region> getCsvForLastWeek()
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
                                region.date = DateTime.Parse(dict2["date"].ToString());
                                region.regionName = regionName;
                                region.confirmedToDate = (dict2["region." + regionName.ToLower() + ".cases.confirmed.todate"].ToString());

                                listOfRegions.Add(region);
                            }
                        }
                    }
                }
            }
            return listOfRegions;
        }
        public List<RegionDTO> getCsvForAll()
        {
            List<RegionDTO> listOfRegions = new List<RegionDTO>();
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
                                RegionDTO region = new RegionDTO();
                                IDictionary<string, object> dict2 = record;
                                region.date = DateTime.Parse(dict2["date"].ToString());
                                region.regionName = regionName;
                                region.active = (dict2["region." + regionName.ToLower() + ".cases.active"]).ToString();
                                region.vaccinatedOnce = (dict2["region." + regionName.ToLower() + ".vaccinated.1st.todate"]).ToString();
                                region.vacinatedTwice = (dict2["region." + regionName.ToLower() + ".vaccinated.2nd.todate"]).ToString();
                                region.deceased = (dict2["region." + regionName.ToLower() + ".deceased.todate"].ToString());
                                listOfRegions.Add(region);
                            }
                        }
                    }
                }
            }
            return listOfRegions;
        }
    }
}
