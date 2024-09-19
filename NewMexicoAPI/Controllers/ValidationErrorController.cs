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
    public class ValidationErrorController : ControllerBase
    {
        private readonly DapperContext _dapperContext;
        private readonly ValidationErrorRepository _validationErrorRepository;

        public ValidationErrorController(DapperContext dapperContext, ValidationErrorRepository validationErrorRepository)
        {
            _dapperContext = dapperContext;
            _validationErrorRepository = validationErrorRepository;
        }

        [HttpGet("Get_Validation_Grid_FriendlyNameByGridID")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<ValidationGridHeaders>))]
        public async Task<ActionResult> Get_Validation_Grid_FriendlyNameByGridID(string gridID)
        {
            return Ok(await _validationErrorRepository.GetValidationGridFriendlyNameByGridID(gridID));
        }

        [HttpGet("Get_Validation_District_RP_GridMain")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<dynamic>))]
        public async Task<ActionResult> Get_Validation_District_RP_GridMain([FromBody] ValidationErrorRequest validationErrorRequest)
        {
            return Ok(await _validationErrorRepository.GetValidationErrorDistrictGridMain(validationErrorRequest));
        }

        [HttpGet("Get_Validation_DistrictSchool_RP_Count_ErrorWarning")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<dynamic>))]
        public async Task<ActionResult> Get_Validation_DistrictSchool_RP_Count_ErrorWarning([FromBody] ValidationErrorRequest validationErrorRequest)
        {
            return Ok(await _validationErrorRepository.GetValidationDistrictSchoolRPCountErrorWarning(validationErrorRequest));
        }

        [HttpGet("Get_Validation_District_RP_Count_ErrorWarning")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<dynamic>))]
        public async Task<ActionResult> Get_Validation_District_RP_Count_ErrorWarning([FromBody] ValidationErrorRequest validationErrorRequest)
        {
            return Ok(await _validationErrorRepository.GetValidationDistrictRPCountErrorWarning(validationErrorRequest));
        }

        [HttpGet("Get_Validation_Schools_RP_Count_ErrorWarning")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<dynamic>))]
        public async Task<ActionResult> Get_Validation_Schools_RP_Count_ErrorWarning([FromBody] ValidationErrorRequest validationErrorRequest)
        {
            return Ok(await _validationErrorRepository.GetValidationSchoolRPCountErrorWarning(validationErrorRequest));
        }

        [HttpGet("Get_Validation_Schools_RP_GridMain")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<dynamic>))]
        public async Task<ActionResult> Get_Validation_Schools_RP_GridMain([FromBody] ValidationErrorRequest validationErrorRequest)
        {
            return Ok(await _validationErrorRepository.GetValidationErrorSchoolGridMain(validationErrorRequest));
        }
    }
}
