using Dapper;
using NewMexicoAPI.Common;
using NewMexicoAPI.Models;
using System.Data.SqlClient;

namespace NewMexicoAPI.Repositories
{
    public class DistrictHomeRepository
    {
        private readonly DapperContext _dapperContext;
        public DistrictHomeRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<SmallDistrictResponse> SmallDistrictCardsResults(DistrictHomeRequest districtRequest)
        {
            //string closeDate = "1900-01-01";

            string closeDate = districtRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");

            string queryString = @"select [dbo].[fn_Validation_RP_Card_District_Count_Errors](@ReportingPeriodName,@CloseDate,@DistrictRefId);
            select [dbo].[fn_Validation_RP_Card_District_Count_Warnings](@ReportingPeriodName,@CloseDate,@DistrictRefId);
            select [dbo].[fn_Validation_RP_Card_District_SchoolsCount_WithErrors](@ReportingPeriodName,@CloseDate,@DistrictRefId);
            select [dbo].[fn_Validation_RP_Card_District_SchoolsCount_WithWarnings](@ReportingPeriodName,@CloseDate,@DistrictRefId);
            select [dbo].[fn_Validation_RP_Card_District_SchoolsCount_WithZeroErrors](@ReportingPeriodName,@CloseDate,@DistrictRefId);
            select [dbo].[fn_Validation_RP_Card_District_SchoolsCount_WithZeroWarnings](@ReportingPeriodName,@CloseDate,@DistrictRefId)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", districtRequest.ReportingPeriodName);
            param.Add("@CloseDate", formattedDate);
            param.Add("@DistrictRefId", districtRequest.DistrictRefId);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                using (var multi = connection.QueryMultiple(queryString, param))
                {
                    var districtHomeResponse = new SmallDistrictResponse();
                    districtHomeResponse.DistrictCountErrors = await multi.ReadFirstAsync<string>();
                    districtHomeResponse.DistrictCountWarnings = await multi.ReadFirstAsync<string>();
                    districtHomeResponse.DistrictSchoolsCountWithErrors = await multi.ReadFirstAsync<string>();
                    districtHomeResponse.DistrictSchoolsCountWithWarnings = await multi.ReadFirstAsync<string>();
                    districtHomeResponse.DistrictSchoolsCountWithZeroErrors = await multi.ReadFirstAsync<string>();
                    districtHomeResponse.DistrictSchoolsCountWithZeroWarnings = await multi.ReadFirstAsync<string>();
                    return districtHomeResponse;
                }
            }
        }

        public async Task<MediumDistrictResponse> MediumDistrictCardsResults(DistrictHomeRequest districtRequest)
        {
            string queryString = @"select * from [dbo].[Validation_Agg_DistrictLevel_RP_Card_SchoolsErrors](@ReportingPeriodName,@DistrictRefId);
                                select * from [dbo].[Validation_Agg_DistrictLevel_RP_Card_ErrorGroups](@ReportingPeriodName,@DistrictRefId);
                                select * from [dbo].[Validation_Agg_DistrictLevel_RP_Card_SchoolsZeroErrors](@ReportingPeriodName,@DistrictRefId);";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", districtRequest.ReportingPeriodName);
            param.Add("@DistrictRefId", districtRequest.DistrictRefId);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                using (var multi = connection.QueryMultiple(queryString, param))
                {
                    var mediumDistrictResponse = new MediumDistrictResponse();
                    mediumDistrictResponse.SchoolErrorList = await multi.ReadAsync<SchoolErrors>();
                    mediumDistrictResponse.SchoolErrorGroupsList = await multi.ReadAsync<SchoolErrorGroups>();
                    mediumDistrictResponse.SchoolsZeroErrorsList = await multi.ReadAsync<SchoolsZeroErrors>();
                    return mediumDistrictResponse;
                }
            }
        }

        public async Task<IEnumerable<LargeDistrictResponse>> LargeDistrictCardsResults(DistrictHomeRequest districtRequest)
        {
            string queryString = "select * from [dbo].[Validation_Agg_DistrictLevel_RP_Card_ErrorList](@ReportingPeriodName,@DistrictRefId)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", districtRequest.ReportingPeriodName);
            param.Add("@DistrictRefId", districtRequest.DistrictRefId);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                var response = con.Read<LargeDistrictResponse>().AsList();
                return response;
            }
        }

        public async Task<IEnumerable<DistrictControl>> GetAllIsActiveListDistrictControl()
        {
            string queryString = "select * from [dbo].[Validation_Get_All_isActiveList_DistrictControl]()";
            var param = new DynamicParameters();

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, commandType: System.Data.CommandType.Text);
                var response = con.Read<DistrictControl>().AsList();
                return response;
            }
        }

        public async Task<string> GetSSORoleRefID(string ssoUserId, string ssoDistrictId, string ssoRole)
        {
            string queryString = "select [dbo].[fn_Validation_GetUserRole](@RoleList)";
            var param = new DynamicParameters();
            param.Add("@RoleList", ssoRole);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                using (var multi = connection.QueryMultiple(queryString, param))
                {
                    var SSORoleXML = await multi.ReadFirstAsync<string>();
                    return SSORoleXML;
                }
            }
        }

        public string GetDistrictRefIdByDistrictCode(string districtCode)
        {
            string queryString = "SELECT * FROM Validation_Get_DistrictName_By_DistrictId(@DistrictId)";

            SqlParameter[] sqlparamData = new SqlParameter[]
                { new SqlParameter("@DistrictId", districtCode)
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
                        {
                            var districtRefId = reader["DistrictRefId"].ToString();
                            return districtRefId;
                        }
                        else
                            return null;
                    }
                }
                finally
                {
                    reader.Close();
                    connection2.Close();
                }
            }
            return null;
        }
    }
}
