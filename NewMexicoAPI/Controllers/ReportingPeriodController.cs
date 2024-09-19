using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewMexicoAPI.Models;
using NewMexicoAPI.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace NewMexicoAPI.Controllers
{
    [RequireHttps]
    [Route("api")]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ReportingPeriodController : ControllerBase
    {
        private readonly ReportingPeriodRepository _reportingPeriodRepository;
        public ReportingPeriodController(ReportingPeriodRepository reportingPeriodRepository)
        {
            _reportingPeriodRepository = reportingPeriodRepository;
        }
        [HttpGet("ValidationDashboard_ReportingPeriodsList")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ReportingPeriodListResponse))]
        public async Task<ActionResult> ValidationDashboard_ReportingPeriodsList()
        {
            return Ok(await _reportingPeriodRepository.GetAllReportingPeriods());
        }
    }
}
