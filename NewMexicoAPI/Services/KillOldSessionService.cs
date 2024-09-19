using NewMexicoAPI.Common;
using NewMexicoAPI.Services.Interfaces;
using System.Data.SqlClient;

namespace NewMexicoAPI.Services
{
    public class KillOldSessionService : IKillOldSessionService
    {
        public void GetSQLData()
        {
            SqlConnection conn = DBConnection.GetDBConnection();

            conn.Open();
            string sqlQuery = "select * from SSO_UserTokenTracker";
            SqlCommand command = new SqlCommand(sqlQuery, conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    //add logic to proceed on table data
                }
            }
        }
    }
}
