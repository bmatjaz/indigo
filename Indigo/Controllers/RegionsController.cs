using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Service.Services;
using Microsoft.AspNetCore.Authorization;

namespace Indigo.Controllers
{
    [Route("api/region/cases")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        protected IRegionService _regionService;

        public RegionsController()
        {
            _regionService = new RegionService();
        }

        [HttpGet]
        public IEnumerable<RegionDTO> Get(string? name = null, string? dateFrom = null, string? dateTo = null)
        {
            return _regionService.getAllCases(name, dateFrom, dateTo);
        }      
    }
}
