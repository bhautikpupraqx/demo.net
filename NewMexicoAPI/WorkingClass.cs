using System.Data.SqlClient;
using System.Text;

namespace NewMexicoAPI
{
    public class WorkingClass
    {
        public static string GeneratePasswordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public WorkingClass()
        {

        }

        public bool Portal_LOG(string UserId, string APIName, string ActionType, string ZoneRoutingTag, string Payload)
        {
            string ReturnString = "";
            bool Success = false;

            try
            {
                SqlParameter[] sqlparam = new SqlParameter[]
                        { new SqlParameter("@UserId", UserId)
                        , new SqlParameter("@APIName", APIName)
                        , new SqlParameter("@ActionType", ActionType)
                        , new SqlParameter("@ZoneRoutingTag", ZoneRoutingTag)
                        , new SqlParameter("@Payload", Payload)
                      };

                Success = new DB_WorkingClass().DB_ExecuteNonQuery_Record(@"INSERT INTO SSO_LogPortal
           (UserId,APIName,ActionType,ZoneRoutingTag,Payload
           )
     VALUES
           (@UserId, @APIName, @ActionType, @ZoneRoutingTag, @Payload)", sqlparam);
            }
            catch (Exception)
            {
                Success = false;
            }

            return Success;

        }

        public string Get_UserLoginId_From_UserToken(string id)
        {
            bool success = false;

            SqlParameter[] sqlparam = new SqlParameter[]
                        { new SqlParameter("@UserToken", id)
                      };

            string queryString2 = "select UserLoginId from SSO_UserTokenTracker with (Nolock) where UserToken = @UserToken";

            string ReturnString = "";
            string UserLoginId = string.Empty;
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
                            UserLoginId = (reader["UserLoginId"].ToString());
                        }
                    }
                    finally
                    {
                        reader.Close();// Always call Close when done reading.
                        connection2.Close();
                    }
                }
            }
            catch (Exception)
            {
                ReturnString = "";
            }

            return UserLoginId;
        }

        public string GeneratePassword(int length)
        {
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)

                res.Append(GeneratePasswordChars[rnd.Next(GeneratePasswordChars.Length)]);
            return res.ToString();
        }
    }
}
