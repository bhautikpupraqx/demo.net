using System.Data.SqlClient;

namespace NewMexicoAPI.Common
{
    public class DBConnection
    {
        public static SqlConnection GetDBConnection()
        {
            var configurationDetails = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection conn = new SqlConnection(CryptoHelper.Decrypt(configurationDetails.GetValue<string>("ConnectionStrings:DefaultConnection2")));
            return conn;
        }
        public static SqlConnection GetDBConnection_EDFI_ODS_Reports()
        {
            var configurationDetails = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection conn = new SqlConnection(CryptoHelper.Decrypt(configurationDetails.GetValue<string>("ConnectionStrings:ODS_Connection")));
            return conn;
        }

        public static SqlConnection GetDBConnection_xDValidator_EdFi()
        {
            var configurationDetails = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlConnection conn = new SqlConnection(CryptoHelper.Decrypt(configurationDetails.GetValue<string>("ConnectionStrings:Validator_Connection")));
            return conn;
        }
    }
}
