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
    public class SchoolHomeController : ControllerBase
    {
        private readonly SchoolHomeRepository _schoolHomeRepository;
        public SchoolHomeController(SchoolHomeRepository schoolHomeRepository)
        {
            _schoolHomeRepository = schoolHomeRepository;
        }

        [HttpGet("SmallSchoolCardsResults")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> SmallSchoolCardsResults([FromBody] SchoolHomeRequest schoolRequest)
        {
            return Ok(await _schoolHomeRepository.SmallSchoolCardsResults(schoolRequest));
        }

        [HttpGet("MediumSchoolCardsResults")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> MediumSchoolCardsResults([FromBody] SchoolHomeRequest schoolRequest)
        {
            return Ok(await _schoolHomeRepository.MediumSchoolCardsResults(schoolRequest));
        }

        [HttpGet("LargeSchoolCardsResults")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> LargeSchoolCardsResults([FromBody] SchoolHomeRequest schoolRequest)
        {
            return Ok(await _schoolHomeRepository.LargeSchoolCardsResults(schoolRequest));
        }

        [HttpGet("GetAllIsActiveListSchoolControl")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> GetAllIsActiveListSchoolControl()
        {
            return Ok(await _schoolHomeRepository.GetAllIsActiveListSchoolControl());
        }
    }
}
