using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using DataAccess.Models;
using Service.Services;

namespace Indigo.Controllers
{
    [Route("api/region/lastweek")]
    [ApiController]
    public class LastWeekController : ControllerBase
    {
        protected IRegionService _regionService;

        public LastWeekController()
        {
            _regionService = new RegionService();
        }

        [HttpGet]
        public IEnumerable<RegionAverageDTO> GetLastWeek()
        {
            return _regionService.getSevenDayAverage();
        }
    }
}
