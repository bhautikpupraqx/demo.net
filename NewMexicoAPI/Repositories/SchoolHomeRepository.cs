using Dapper;
using NewMexicoAPI.Common;
using NewMexicoAPI.Models;

namespace NewMexicoAPI.Repositories
{
    public class SchoolHomeRepository
    {
        private readonly DapperContext _dapperContext;
        public SchoolHomeRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<SmallSchoolResponse> SmallSchoolCardsResults(SchoolHomeRequest schoolRequest)
        {
            //string closeDate = "1900-01-01";

            string closeDate = schoolRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");

            string queryString = @"select [dbo].[fn_Validation_RP_Card_School_SchoolsCount_WithErrors] (@ReportingPeriodName,@CloseDate,@DistrictRefId,@SchoolList)
                    select [dbo].[fn_Validation_RP_Card_School_SchoolsCount_WithWarnings] (@ReportingPeriodName,@CloseDate,@DistrictRefId,@SchoolList)
                    select [dbo].[fn_Validation_RP_Card_School_SchoolsCount_WithZeroErrors] (@ReportingPeriodName,@CloseDate,@DistrictRefId,@SchoolList)
                    select [dbo].[fn_Validation_RP_Card_School_SchoolsCount_WithZeroWarnings] (@ReportingPeriodName,@CloseDate,@DistrictRefId,@SchoolList)
                    ";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", schoolRequest.ReportingPeriodName);
            param.Add("@CloseDate", formattedDate);
            param.Add("@DistrictRefId", schoolRequest.DistrictRefId);
            param.Add("@SchoolList", schoolRequest.SchoolList);

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                using (var multi = connection.QueryMultiple(queryString, param))
                {
                    var schoolHomeResponse = new SmallSchoolResponse();
                    schoolHomeResponse.SchoolCountErrors = await multi.ReadFirstAsync<string>();
                    schoolHomeResponse.SchoolCountWarnings = await multi.ReadFirstAsync<string>();
                    schoolHomeResponse.SchoolsCountWithZeroErrors = await multi.ReadFirstAsync<string>();
                    schoolHomeResponse.SchoolsCountWithZeroWarnings = await multi.ReadFirstAsync<string>();
                    return schoolHomeResponse;
                }
            }
        }

        public async Task<MediumSchoolResponse> MediumSchoolCardsResults(SchoolHomeRequest schoolRequest)
        {
            string queryString = @"select * from [dbo].[Validation_Agg_SchoolLevel_RP_Card_SchoolsErrors](@ReportingPeriodName,@DistrictRefId,@SchoolList);
                            select * from [dbo].[Validation_Agg_SchoolLevel_RP_Card_ErrorGroups](@ReportingPeriodName,@DistrictRefId,@SchoolList);
                            select * from [dbo].[Validation_Agg_SchoolLevel_RP_Card_SchoolsZeroErrors](@ReportingPeriodName,@DistrictRefId,@SchoolList);";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", schoolRequest.ReportingPeriodName);
            param.Add("@DistrictRefId", schoolRequest.DistrictRefId);
            param.Add("@SchoolList", schoolRequest.SchoolList);

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                using (var multi = connection.QueryMultiple(queryString, param))
                {
                    var mediumSchoolResponse = new MediumSchoolResponse();
                    mediumSchoolResponse.SchoolErrorList = await multi.ReadAsync<SchoolErrors>();
                    mediumSchoolResponse.SchoolErrorGroupsList = await multi.ReadAsync<SchoolErrorGroups>();
                    mediumSchoolResponse.SchoolsZeroErrorsList = await multi.ReadAsync<SchoolsZeroErrors>();
                    return mediumSchoolResponse;
                }
            }
        }

        public async Task<IEnumerable<LargeSchoolResponse>> LargeSchoolCardsResults(SchoolHomeRequest schoolRequest)
        {
            string queryString = "select * from [dbo].[Validation_Agg_SchoolLevel_RP_Card_ErrorList](@ReportingPeriodName, @DistrictRefId, @SchoolList)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", schoolRequest.ReportingPeriodName);
            param.Add("@DistrictRefId", schoolRequest.DistrictRefId);
            param.Add("@SchoolList", schoolRequest.SchoolList);

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                var response = con.Read<LargeSchoolResponse>().AsList();
                return response;
            }
        }

        public async Task<IEnumerable<SchoolControl>> GetAllIsActiveListSchoolControl()
        {
            string queryString = "select * from [dbo].[Validation_Get_All_isActiveList_SchoolControl]()";
            var param = new DynamicParameters();

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, commandType: System.Data.CommandType.Text);
                var response = con.Read<SchoolControl>().AsList();
                return response;
            }
        }
    }
}
