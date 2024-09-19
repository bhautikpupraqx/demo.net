using NewMexicoAPI.Common;
using NewMexicoAPI.Models;
using NewMexicoAPI.Services.Interfaces;
using System.Data.SqlClient;

namespace NewMexicoAPI.Services
{
    public class UserService : IUserService
    {
        public bool IsValidUserInformation(LoginModel model)
        {
            var configurationDetails = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            SqlConnection conn = new SqlConnection(CryptoHelper.Decrypt(configurationDetails.GetValue<string>("ConnectionStrings:ODS_Connection")));
            string sqlQuery = "select count(*) from Validation_Dashboard_APIBToken where UserName=@0 AND PasswordHash=@1";
            int count = 0;

            SqlCommand command = new SqlCommand(sqlQuery, conn);

            using (SqlCommand cmdCount = new SqlCommand(sqlQuery, conn))
            {
                cmdCount.Parameters.AddWithValue("@0", model.UserName);
                cmdCount.Parameters.AddWithValue("@1", CryptoHelper.Encrypt(model.Password));

                conn.Open();
                count = (int)cmdCount.ExecuteScalar();
                if (count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public List<LoginModel> GetUserDetails()
        {
            var configurationDetails = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            SqlConnection conn = new SqlConnection(CryptoHelper.Decrypt(configurationDetails.GetValue<string>("ConnectionStrings:DefaultConnection")));
            conn.Open();
            string sqlQuery = "select * from Validation_Dashboard_APIBToken";
            SqlCommand command = new SqlCommand(sqlQuery, conn);

            List<LoginModel> loginModelList = new List<LoginModel>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    loginModelList.Add(new LoginModel
                    {
                        UserName = reader[1].ToString(),
                        Password = CryptoHelper.Decrypt(reader[2].ToString())
                    });
                }
            }

            return loginModelList;
        }

        public bool AddUserDetails(string username, string password)
        {
            try
            {
                var configurationDetails = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

                SqlConnection conn = new SqlConnection(CryptoHelper.Decrypt(configurationDetails.GetValue<string>("ConnectionStrings:DefaultConnection")));
                string sqlQuery = "select count(*) from Validation_Dashboard_APIBToken where UserName=@0";

                int count = 0;

                SqlCommand command = new SqlCommand(sqlQuery, conn);
                using (SqlCommand cmdCount = new SqlCommand(sqlQuery, conn))
                {
                    cmdCount.Parameters.AddWithValue("@0", username);
                    conn.Open();
                    count = (int)cmdCount.ExecuteScalar();
                    if (count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        SqlCommand insertCommand = new SqlCommand("INSERT INTO Validation_Dashboard_APIBToken (UserName, PasswordHash) VALUES (@0, @1)", conn);

                        insertCommand.Parameters.Add(new SqlParameter("0", username));
                        insertCommand.Parameters.Add(new SqlParameter("1", CryptoHelper.Encrypt(password)));

                        insertCommand.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
