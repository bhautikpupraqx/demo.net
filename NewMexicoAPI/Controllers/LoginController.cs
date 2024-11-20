using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewMexicoAPI.Helper;
using NewMexicoAPI.Models;
using NewMexicoAPI.Repositories;
using System.Data.SqlClient;

namespace NewMexicoAPI.Controllers
{
    [RequireHttps]
    [Route("api")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class LoginController : Controller
    {
        public readonly ILogger _logger;
        private readonly ValidationErrorRepository _validationErrorRepository;
        private readonly DashBoardCategoriesRepository _dashBoardCategoriesRepository;
        private readonly EmailNotificationRepository _emailNotificationRepository;

        public LoginController(ILogger<LoginController> logger, ValidationErrorRepository validationErrorRepository, DashBoardCategoriesRepository dashBoardCategoriesRepository, EmailNotificationRepository emailNotificationRepository)
        {
            _logger = logger;
            _validationErrorRepository = validationErrorRepository;
            _dashBoardCategoriesRepository = dashBoardCategoriesRepository;
            _emailNotificationRepository = emailNotificationRepository;
        }

        [HttpGet("AltLogin")]
        public async Task<JsonResult> Login([FromBody] LoginModel loginModel)
        {
            ValidationTrackLoginsModel TrackLoginsModel = new ValidationTrackLoginsModel();
            try
            {
                _logger.LogInformation("AltLogin API called");
                if (loginModel != null && loginModel.UserName != null && loginModel.Password != null)
                {
                    var configurationDetails = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    List<LoggedInUserDetails> allLoggedInUserData = configurationDetails
                                            .GetSection("loggedInUserDetailsData")
                                            .Get<List<LoggedInUserDetails>>();

                    LoggedInUserDetails loggedInUserData = allLoggedInUserData.Where(x => x.UserName == (PasswordHelper.Decrypt(loginModel.UserName)).ToLower() && x.Password == loginModel.Password).FirstOrDefault();
                    
                    if (loggedInUserData != null)
                    {
                        loggedInUserData.Password = null;
                        // Get DistrictRefId
                        string queryString = "SELECT * FROM Validation_Get_DistrictName_By_DistrictId(@DistrictId)";

                        SqlParameter[] sqlparamData = new SqlParameter[]
                            { new SqlParameter("@DistrictId", loggedInUserData.DistrictId)
                          };

                        using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection_EDFI_ODS_Reports())
                        {
                            SqlCommand command2 = new SqlCommand(queryString, connection2);
                            if (sqlparamData != null) command2.Parameters.AddRange(sqlparamData);

                            connection2.Open();

                            SqlDataReader reader = command2.ExecuteReader();
                            try
                            {
                                while (reader.Read())
                                {
                                    if (reader["DistrictRefId"] != null)
                                        loggedInUserData.DistrictRefId = reader["DistrictRefId"].ToString();
                                }
                            }
                            finally
                            {
                                reader.Close();
                                connection2.Close();
                            }
                        }

                        if (loggedInUserData.Schools != "AllSchools")
                        {
                            List<SchoolControl> schoolList = await _validationErrorRepository.GetSchoolListByDistrictRefId(loggedInUserData.DistrictRefId);
                            if (schoolList != null && schoolList.Count > 0)
                            {
                                var schoolIdsFromAppData = loggedInUserData.Schools.Split(",").ToArray();
                                loggedInUserData.SchoolRefIds = string.Join(",", schoolList.Where(x => schoolIdsFromAppData.Contains(x.SchoolID.ToString())).Select(x => x.SchoolRefId).ToArray());
                            }
                            else
                            {
                                loggedInUserData.SchoolRefIds = "";
                            }
                        }

                        if (loggedInUserData != null) {
                            TrackLoginsModel.UserID = "";
                            TrackLoginsModel.FirstName = loggedInUserData.UserName;
                            TrackLoginsModel.LastName = "";
                            TrackLoginsModel.OrgId = loggedInUserData.DistrictId;
                            TrackLoginsModel.Role = loggedInUserData.Role;
                            TrackLoginsModel.LoginType = "Altlogin";
                            TrackLoginsModel.AccessProfile = "";
                         }
                        
                       bool checkemail = _emailNotificationRepository.IsValidEmail(loggedInUserData.Email);
                        if(checkemail == true)
                        {
                            _emailNotificationRepository.TrackEmail(loggedInUserData);
                        }
                        else
                        {
                            _logger.LogInformation("Email is not valid");
                        }
                        
                        //_dashBoardCategoriesRepository.Validation_TrackLogins(TrackLoginsModel);

                        return Json(loggedInUserData);
                    }
                }
                return Json("model empty");
            }
            catch (Exception ex)
            {
                return Json("model empty");
            }

        }
    }
}
