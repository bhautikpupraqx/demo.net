using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewMexicoAPI.Models;
using NewMexicoAPI.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace NewMexicoAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.RequireHttps]
    [Route("api")]
    public class DistrictHomeController : ControllerBase
    {
        private readonly DistrictHomeRepository _districtHomeRepository;
        public DistrictHomeController(DistrictHomeRepository districtHomeRepository)
        {
            _districtHomeRepository = districtHomeRepository;
        }

        [HttpGet("SmallDistrictCardsResults")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> SmallDistrictCardsResults([FromBody] DistrictHomeRequest districtRequest)
        {
            return Ok(await _districtHomeRepository.SmallDistrictCardsResults(districtRequest));
        }

        [HttpGet("MediumDistrictCardsResults")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> MediumDistrictCardsResults([FromBody] DistrictHomeRequest districtRequest)
        {
            return Ok(await _districtHomeRepository.MediumDistrictCardsResults(districtRequest));
        }

        [HttpGet("LargeDistrictCardsResults")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> LargeDistrictCardsResults([FromBody] DistrictHomeRequest districtRequest)
        {
            return Ok(await _districtHomeRepository.LargeDistrictCardsResults(districtRequest));
        }

        [HttpGet("GetAllIsActiveListDistrictControl")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> GetAllIsActiveListDistrictControl()
        {
            return Ok(await _districtHomeRepository.GetAllIsActiveListDistrictControl());
        }
    }
}
