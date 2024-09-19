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
    public class StateHomeController : ControllerBase
    {
        private readonly StateHomeRepository _stateHomeRepoitory;
        public StateHomeController(StateHomeRepository stateHomeRepository)
        {
            _stateHomeRepoitory = stateHomeRepository;
        }

        [HttpGet("Get_Validation_Agg_StateLevel_RP_Card_ErrorList")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<ErrorResponse>))]
        public async Task<ActionResult> Get_Validation_Agg_StateLevel_RP_Card_ErrorList([FromBody] SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            return Ok(await _stateHomeRepoitory.GetValidationAggStateLevelRPCardErrorList(selectedStateHomePageRequest));
        }

        [HttpGet("Get_Validation_Agg_StateLevel_RP_Card_ErrorGroups")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<ErrorGroups>))]
        public async Task<ActionResult> Get_Validation_Agg_StateLevel_RP_Card_ErrorGroups([FromBody] SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            return Ok(await _stateHomeRepoitory.GetValidationAggStateLevelRPCardErrorGroups(selectedStateHomePageRequest));
        }

        [HttpGet("Get_Validation_Agg_StateLevel_RP_Card_DistrictZeroErrors")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<DistrictZeroErrors>))]
        public async Task<ActionResult> Get_Validation_Agg_StateLevel_RP_Card_DistrictZeroErrors([FromBody] SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            return Ok(await _stateHomeRepoitory.GetValidationAggStateLevelRPCardDistrictZeroErrors(selectedStateHomePageRequest));
        }

        [HttpGet("Get_Validation_Agg_StateLevel_RP_Card_DistrictErrors")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<DistrictErrors>))]
        public async Task<ActionResult> Get_Validation_Agg_StateLevel_RP_Card_DistrictErrors([FromBody] SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            return Ok(await _stateHomeRepoitory.GetValidationAggStateLevelRPCardDistrictErrors(selectedStateHomePageRequest));
        }

        [HttpGet("Get_Validation_RP_Card_State_All_Errors")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<string>))]
        public async Task<ActionResult> Get_Validation_RP_Card_State_All_Errors([FromBody] SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            return Ok(await _stateHomeRepoitory.GetValidationRPCardStateAllErrors(selectedStateHomePageRequest));
        }

        [HttpGet("Get_fn_Validation_RP_Card_State_All_Warnings")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<DistrictErrors>))]
        public async Task<ActionResult> Get_fn_Validation_RP_Card_State_All_Warnings([FromBody] SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            return Ok(await _stateHomeRepoitory.GetfnValidationRPCardStateAllWarnings(selectedStateHomePageRequest));
        }

        [HttpGet("Get_fn_Validation_RP_Card_State_DistrictsCount_WithErrors")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> Get_fn_Validation_RP_Card_State_DistrictsCount_WithErrors([FromBody] SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            return Ok(await _stateHomeRepoitory.GetfnValidationRPCardStateDistrictsCountWithErrors(selectedStateHomePageRequest));
        }

        [HttpGet("Get_fn_Validation_RP_Card_State_DistrictsCount_WithWarnings")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> Get_fn_Validation_RP_Card_State_DistrictsCount_WithWarnings([FromBody] SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            return Ok(await _stateHomeRepoitory.GetfnValidationRPCardStateDistrictsCountWithWarnings(selectedStateHomePageRequest));
        }

        [HttpGet("Get_fn_Validation_RP_Card_State_DistrictsCount_WithZeroErrors")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> Get_fn_Validation_RP_Card_State_DistrictsCount_WithZeroErrors([FromBody] SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            return Ok(await _stateHomeRepoitory.GetfnValidationRPCardStateDistrictsCountWithZeroErrors(selectedStateHomePageRequest));
        }

        [HttpGet("Get_fn_Validation_RP_Card_State_SchoolsCount_WithErrors")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> Get_fn_Validation_RP_Card_State_SchoolsCount_WithErrors([FromBody] SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            return Ok(await _stateHomeRepoitory.GetfnValidationRPCardStateSchoolsCountWithErrors(selectedStateHomePageRequest));
        }

        [HttpGet("Get_fn_Validation_RP_Card_State_SchoolsCount_WithWarnings")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        public async Task<ActionResult> Get_fn_Validation_RP_Card_State_SchoolsCount_WithWarnings([FromBody] SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            return Ok(await _stateHomeRepoitory.GetfnValidationRPCardStateSchoolsCountWithWarnings(selectedStateHomePageRequest));
        }
    }
}
