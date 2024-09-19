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
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class Dashboard_CategoriesController : ControllerBase
    {
        private readonly DashBoardCategoriesRepository _dashBoardCategoriesRepository;
        public Dashboard_CategoriesController(DashBoardCategoriesRepository dashBoardCategoriesRepository)
        {
            _dashBoardCategoriesRepository = dashBoardCategoriesRepository;
        }
        [HttpGet("ValidationDashboard_CategoriesList")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValidationDashboard_CategoriesList))]
        public async Task<ActionResult> ValidationDashboard_CategoriesList()
        {
            return Ok(await _dashBoardCategoriesRepository.GetAllValidationDashboardCategories());
        }
    }
}
