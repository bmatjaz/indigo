using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Indigo.Models;
using Indigo.Services;

namespace Indigo.Controllers
{
    [Route("api/region/lastweek")]
    [ApiController]
    public class LastWeekController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<RegionAverageDTO> GetLastWeek()
        {
            Service service = new Service();
            List<RegionAverageDTO> listOfRegions = service.getSevenDayAverage();

            return listOfRegions;
        }
    }
}
