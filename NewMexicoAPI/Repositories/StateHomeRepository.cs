using Dapper;
using NewMexicoAPI.Common;
using NewMexicoAPI.Models;

namespace NewMexicoAPI.Repositories
{
    public class StateHomeRepository
    {
        private readonly DapperContext _dapperContext;

        public StateHomeRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<ErrorResponse>> GetValidationAggStateLevelRPCardErrorList(SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            string queryString = "select * from [dbo].[Validation_Agg_StateLevel_RP_Card_ErrorList](@ReportingPeriodName) order by ErrorCount DESC";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", selectedStateHomePageRequest.ReportingPeriodName);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                var listErrorReponse = con.Read<ErrorResponse>().AsList();
                return listErrorReponse;
            }
        }

        public async Task<IEnumerable<ErrorGroups>> GetValidationAggStateLevelRPCardErrorGroups(SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            string queryString = "select * from [dbo].[Validation_Agg_StateLevel_RP_Card_ErrorGroups](@ReportingPeriodName)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", selectedStateHomePageRequest.ReportingPeriodName);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                var listerrorGroups = con.Read<ErrorGroups>().AsList();
                return listerrorGroups;
            }
        }

        public async Task<IEnumerable<DistrictZeroErrors>> GetValidationAggStateLevelRPCardDistrictZeroErrors(SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            string queryString = "select * from [dbo].[Validation_Agg_StateLevel_RP_Card_DistrictZeroErrors](@ReportingPeriodName)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", selectedStateHomePageRequest.ReportingPeriodName);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                var listDistrictZeroErrors = con.Read<DistrictZeroErrors>().AsList();
                return listDistrictZeroErrors;
            }
        }

        public async Task<IEnumerable<DistrictErrors>> GetValidationAggStateLevelRPCardDistrictErrors(SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            string queryString = "select * from [dbo].[Validation_Agg_StateLevel_RP_Card_DistrictErrors](@ReportingPeriodName) order by ErrorCount DESC";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", selectedStateHomePageRequest.ReportingPeriodName);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                var listDistrictErrors = con.Read<DistrictErrors>().AsList();
                return listDistrictErrors;
            }
        }

        public async Task<string> GetValidationRPCardStateAllErrors(SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            string closeDate = selectedStateHomePageRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");

            string queryString = "select [dbo].[fn_Validation_RP_Card_State_All_Errors](@ReportingPeriodName, @CloseDate)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", selectedStateHomePageRequest.ReportingPeriodName);
            param.Add("@CloseDate", formattedDate);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var error = await connection.QueryAsync<string>(queryString, param: param, commandType: System.Data.CommandType.Text);
                return error.FirstOrDefault().ToString();
            }
        }

        public async Task<string> GetfnValidationRPCardStateAllWarnings(SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            string closeDate = selectedStateHomePageRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");

            string queryString = "select [dbo].[fn_Validation_RP_Card_State_All_Warnings](@ReportingPeriodName, @CloseDate)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", selectedStateHomePageRequest.ReportingPeriodName);
            param.Add("@CloseDate", formattedDate);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var error = await connection.QueryAsync<string>(queryString, param: param, commandType: System.Data.CommandType.Text);
                return error.FirstOrDefault().ToString();
            }
        }

        public async Task<string> GetfnValidationRPCardStateDistrictsCountWithErrors(SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            string closeDate = selectedStateHomePageRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");

            string queryString = "select [dbo].[fn_Validation_RP_Card_State_DistrictsCount_WithErrors](@ReportingPeriodName, @CloseDate)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", selectedStateHomePageRequest.ReportingPeriodName);
            param.Add("@CloseDate", formattedDate);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var error = await connection.QueryAsync<string>(queryString, param: param, commandType: System.Data.CommandType.Text);
                return error.FirstOrDefault().ToString();
            }
        }

        public async Task<string> GetfnValidationRPCardStateDistrictsCountWithWarnings(SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            string closeDate = selectedStateHomePageRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");

            string queryString = "select [dbo].[fn_Validation_RP_Card_State_DistrictsCount_WithWarnings](@ReportingPeriodName, @CloseDate)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", selectedStateHomePageRequest.ReportingPeriodName);
            param.Add("@CloseDate", formattedDate);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var error = await connection.QueryAsync<string>(queryString, param: param, commandType: System.Data.CommandType.Text);
                return error.FirstOrDefault().ToString();
            }
        }

        public async Task<string> GetfnValidationRPCardStateDistrictsCountWithZeroErrors(SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            string closeDate = selectedStateHomePageRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");

            string queryString = "select [dbo].[fn_Validation_RP_Card_State_DistrictsCount_WithZeroErrors](@ReportingPeriodName, @CloseDate)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", selectedStateHomePageRequest.ReportingPeriodName);
            param.Add("@CloseDate", formattedDate);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var error = await connection.QueryAsync<string>(queryString, param: param, commandType: System.Data.CommandType.Text);
                return error.FirstOrDefault().ToString();
            }
        }

        public async Task<string> GetfnValidationRPCardStateSchoolsCountWithErrors(SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            string closeDate = selectedStateHomePageRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");

            string queryString = "select [dbo].[fn_Validation_RP_Card_State_SchoolsCount_WithErrors](@ReportingPeriodName, @CloseDate)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", selectedStateHomePageRequest.ReportingPeriodName);
            param.Add("@CloseDate", formattedDate);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var error = await connection.QueryAsync<string>(queryString, param: param, commandType: System.Data.CommandType.Text);
                return error.FirstOrDefault().ToString();
            }
        }

        public async Task<string> GetfnValidationRPCardStateSchoolsCountWithWarnings(SelectedStateHomePageRequest selectedStateHomePageRequest)
        {
            string closeDate = selectedStateHomePageRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");

            string queryString = "select [dbo].[fn_Validation_RP_Card_State_SchoolsCount_WithWarnings](@ReportingPeriodName, @CloseDate)";
            var param = new DynamicParameters();
            param.Add("@ReportingPeriodName", selectedStateHomePageRequest.ReportingPeriodName);
            param.Add("@CloseDate", formattedDate);
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var error = await connection.QueryAsync<string>(queryString, param: param, commandType: System.Data.CommandType.Text);
                return error.FirstOrDefault().ToString();
            }
        }
    }
}
