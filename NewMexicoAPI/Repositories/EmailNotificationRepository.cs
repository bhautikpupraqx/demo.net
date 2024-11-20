using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewMexicoAPI.Common;
using NewMexicoAPI.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace NewMexicoAPI.Repositories
{
    public class EmailNotificationRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IConfiguration _configuration;
        public EmailNotificationRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public bool IsValidEmail(string email)
        {
            // Regular expression for more complex email validation
            string pattern = @"^(?!\.)([a-zA-Z0-9]+(\.[a-zA-Z0-9]+)*)@(?!\-)([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,}$"
;
            bool checkemail =  Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);

            return checkemail;
        }

        [HttpPost("track-email")]
        public async Task TrackEmail(LoggedInUserDetails loggedInUserDetails)
        {
            try
            {
                var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports();

                using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_EmailNotification_TrackEmail", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add stored procedure parameters
                        cmd.Parameters.AddWithValue("@SSO_UserID", loggedInUserDetails.UserName);
                        cmd.Parameters.AddWithValue("@SSO_Email", loggedInUserDetails.Email);
                        cmd.Parameters.AddWithValue("@SSO_Role", loggedInUserDetails.Role);

                        // Output parameter
                        SqlParameter returnValue = new SqlParameter();
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(returnValue);

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        int result = (int)returnValue.Value;

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }


        public async Task<IEnumerable<EmailTemplateForUsersModel>> EmailNotificationTemplateForUsers(string role, string userid)
        {
            try
            {
                string queryString = "SELECT * FROM [dbo].[fn_EmailNotification_Templates_For_Users](@SSO_Role, @SSO_UserID)"; // Fixed parameter order
                var param = new DynamicParameters();
                param.Add("@SSO_Role", role);
                param.Add("@SSO_UserID", userid); // Ensure this matches the stored procedure parameter name

                using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
                {
                    // Pass the parameters to QueryMultipleAsync
                    var con = await connection.QueryMultipleAsync(queryString, param, commandType: System.Data.CommandType.Text);
                    var templateforemailnotification = con.Read<EmailTemplateForUsersModel>().AsList();
                    return templateforemailnotification;
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                var model = new List<EmailTemplateForUsersModel>();
                return model; // Return an empty list in case of error
            }
        }

        public async Task<SaveEmailTemplate> SaveEmailnotificationtemplatedata(SaveEmailTemplate saveEmailTemplate)
        {
            try
            {
                var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports();

                using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_EmailNotification_Templates_User_Associations", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("SSO_UserID", saveEmailTemplate.UserId);
                        cmd.Parameters.AddWithValue("@TemplateKey", saveEmailTemplate.template_id);
                        cmd.Parameters.AddWithValue("@flag", saveEmailTemplate.flag);

                        SqlParameter returnValue = new SqlParameter();
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(returnValue);

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        int result = (int)returnValue.Value;

                        return saveEmailTemplate;
                    }
                }
            }
            catch (Exception ex)
            {
                return saveEmailTemplate;
            }
        }

    }
}

