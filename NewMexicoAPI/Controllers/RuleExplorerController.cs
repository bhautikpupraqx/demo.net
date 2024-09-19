using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewMexicoAPI.Common;
using NewMexicoAPI.Models;
using NewMexicoAPI.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace NewMexicoAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.RequireHttps]
    [Route("api")]
    public class RuleExplorerController : ControllerBase
    {
        private readonly DapperContext _dapperContext;
        private readonly RuleExplorerRepository _ruleExplorerRepository;

        public RuleExplorerController(DapperContext dapperContext, RuleExplorerRepository ruleExplorerRepository)
        {
            _dapperContext = dapperContext;
            _ruleExplorerRepository = ruleExplorerRepository;
        }

        [HttpGet("Get_ValidationDashboard_Items_By_ReportingPeriod_List")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<dynamic>))]
        public async Task<ActionResult> Get_ValidationDashboard_Items_By_ReportingPeriod_List([FromBody] ValidationErrorRequest validationErrorRequest)
        {
            return Ok(await _ruleExplorerRepository.GetRuleExplorerData(validationErrorRequest));
        }
    }
}
