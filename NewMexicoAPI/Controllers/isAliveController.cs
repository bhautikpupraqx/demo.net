using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewMexicoAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Data.SqlClient;
using System.Net;

namespace NewMexicoAPI.Controllers
{
    [RequireHttps]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class isAliveController : Controller
    {
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserIsAlive))]
        public ActionResult Get(string id)
        {
            return Json(new { isUserTokenAlive = Check_UserToken_IsAlive(id) });
        }

        private bool Check_UserToken_IsAlive(string id)
        {

            bool success = false;
            string queryString2 = "select top 1 CreateDate,TokenExpirationDate from [dbo].[SSO_UserTokenTracker] with (NOLOCK) where usertoken = @Id and isActive = 1";

            SqlParameter[] sqlparam = new SqlParameter[]
                        { new SqlParameter("@Id", id)
                      };

            string ReturnString = "";
            string str_CreateDate = string.Empty;
            string str_TokenExpirationDate = string.Empty;

            try
            {
                using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection())
                {
                    SqlCommand command2 = new SqlCommand(queryString2, connection2);
                    if (sqlparam != null) command2.Parameters.AddRange(sqlparam);

                    connection2.Open();

                    SqlDataReader reader = command2.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            str_CreateDate = (reader["CreateDate"].ToString());
                            str_TokenExpirationDate = (reader["TokenExpirationDate"].ToString());
                        }

                    }
                    finally
                    {
                        reader.Close();// Always call Close when done reading.
                        connection2.Close();
                    }

                    if (string.IsNullOrEmpty(str_CreateDate) || string.IsNullOrEmpty(str_TokenExpirationDate))
                    {
                        success = false;
                    }
                    else
                    {
                        DateTime CreateDate = Convert.ToDateTime(str_CreateDate);
                        DateTime TokenExpirationDate = Convert.ToDateTime(str_TokenExpirationDate);
                        DateTime dt_Now = DateTime.Now;

                        if (dt_Now > CreateDate && dt_Now < TokenExpirationDate)
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                ReturnString = "";

            }

            return success;
        }
    }
}
