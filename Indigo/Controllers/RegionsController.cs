using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Indigo.Models;
using CsvHelper;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Collections;
using FileHelpers;
using Indigo.Services;

namespace Indigo.Controllers
{
    [Route("api/region/cases")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Region> Get(string? name = null, string? dateFrom = null, string? dateTo = null)
        {
            Service service = new Service();
            List<Region> listOfRegions = service.getCsvData(name, dateFrom, dateTo);
            return listOfRegions;
        }      
    }
}
