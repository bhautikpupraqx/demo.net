using System.Data.SqlClient;
using System.Data;

namespace NewMexicoAPI.Common
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _connectionString_EDFI_ODS_Reports;
        private readonly string _connectionString_xDValidator_EdFi;


        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = CryptoHelper.Decrypt(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection2"));
            _connectionString_EDFI_ODS_Reports = CryptoHelper.Decrypt(_configuration.GetValue<string>("ConnectionStrings:ODS_Connection"));
            _connectionString_xDValidator_EdFi = CryptoHelper.Decrypt(_configuration.GetValue<string>("ConnectionStrings:Validator_Connection"));
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
        public IDbConnection CreateConnection_EDFI_ODS_Reports()
            => new SqlConnection(_connectionString_EDFI_ODS_Reports);
        public IDbConnection CreateConnection_xDValidator_EdFi()
            => new SqlConnection(_connectionString_xDValidator_EdFi);
    }
}
