using Dapper;
using NewMexicoAPI.Common;
using NewMexicoAPI.Models;

namespace NewMexicoAPI.Repositories
{
    public class ValidationErrorRepository
    {
        private readonly DapperContext _dapperContext;

        public ValidationErrorRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<dynamic> GetValidationErrorDistrictGridMain(ValidationErrorRequest validationErrorRequest)
        {
            string queryString = "";
            string closeDate = validationErrorRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");
            if (formattedDate == "1900-01-01")
            {
                formattedDate = "9999-12-28";
            }

            if (validationErrorRequest.ValidationErrorType == "Errors")
            {
                queryString = @"select * from  [dbo].[Validation_District_RP_GridMain_Error](@DistrictRefID,@RP,@CloseDate)" +
                                @"UNION 
                                select* from[dbo].[Validation_District_RP_GridMain_NonFatalError] (@DistrictRefID, @RP,@CloseDate)";
            }
            else if (validationErrorRequest.ValidationErrorType == "Warnings")
            {
                queryString = "select * from  [dbo].[Validation_District_RP_GridMain_Warning](@DistrictRefID,@RP,@CloseDate)";
            }
            else
            {
                queryString = "select * from  [dbo].[Validation_District_RP_GridMain](@DistrictRefID,@RP,@CloseDate)";
            }

            var param = new DynamicParameters();
            param.Add("@DistrictRefID", validationErrorRequest.DistrictRefId);
            param.Add("@RP", validationErrorRequest.ReprotingPeriodName);
            param.Add("@CloseDate", formattedDate);

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                dynamic listValidationError = con.Read().AsList();
                return listValidationError;
            }
        }

        public async Task<IEnumerable<ValidationGridHeaders>> GetValidationGridFriendlyNameByGridID(string gridID)
        {
            List<ValidationGridHeaders> listValidationGridHeaders = new List<ValidationGridHeaders>();
            string queryString = "select * from [dbo].[Validation_Grid_FriendlyNameByGridID](@GridID)";

            var param = new DynamicParameters();
            param.Add("@GridID", gridID);

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                var listValidationError = con.Read<ValidationGridHeaders>().AsList().OrderBy(x => x.SortOrder);
                return listValidationError;
            }
        }

        public async Task<dynamic> GetValidationDistrictSchoolRPCountErrorWarning(ValidationErrorRequest validationErrorRequest)
        {
            string closeDate = validationErrorRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");
            if (formattedDate == "1900-01-01")
            {
                formattedDate = "9999-12-28";
            }

            string queryString = "select * from [dbo].[Validation_DistrictSchool_RP_Count_ErrorWarning](@DistrictRefid,@ReportingPeriod,@CloseDate)";
            var param = new DynamicParameters();
            param.Add("@DistrictRefID", validationErrorRequest.DistrictRefId);
            param.Add("@ReportingPeriod", validationErrorRequest.ReprotingPeriodName);
            param.Add("@CloseDate", formattedDate);

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                dynamic listValidationTotalErrorsAndWanings = con.Read().AsList();
                return listValidationTotalErrorsAndWanings;
            }
        }

        public async Task<dynamic> GetValidationDistrictRPCountErrorWarning(ValidationErrorRequest validationErrorRequest)
        {
            string closeDate = validationErrorRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");
            if (formattedDate == "1900-01-01")
            {
                formattedDate = "9999-12-28";
            }

            string queryString = "select * from [dbo].[Validation_District_RP_Count_ErrorWarning](@DistrictRefid,@ReportingPeriod,@CloseDate) ORDER BY Name";
            var param = new DynamicParameters();
            param.Add("@DistrictRefID", validationErrorRequest.DistrictRefId);
            param.Add("@ReportingPeriod", validationErrorRequest.ReprotingPeriodName);
            param.Add("@CloseDate", formattedDate);

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                dynamic listValidationTotalErrorsAndWanings = con.Read().AsList();
                return listValidationTotalErrorsAndWanings;
            }
        }

        public async Task<dynamic> GetValidationSchoolRPCountErrorWarning(ValidationErrorRequest validationErrorRequest)
        {
            string closeDate = validationErrorRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");
            if (formattedDate == "1900-01-01")
            {
                formattedDate = "9999-12-28";
            }

            string queryString = "select * from [dbo].[Validation_Schools_RP_Count_ErrorWarning](@DistrictRefId,@ReportingPeriod,@CloseDate,@SchoolList)";
            var param = new DynamicParameters();
            param.Add("@DistrictRefID", validationErrorRequest.DistrictRefId);
            param.Add("@ReportingPeriod", validationErrorRequest.ReprotingPeriodName);
            param.Add("@CloseDate", formattedDate);
            param.Add("@SchoolList", validationErrorRequest.SchoolList);

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                dynamic listValidationTotalErrorsAndWanings = con.Read().AsList();
                return listValidationTotalErrorsAndWanings;
            }
        }

        public async Task<dynamic> Get_SchoolList_By_DistrictRefId(ValidationErrorRequest validationErrorRequest)
        {
            string queryString = "";
            string closeDate = validationErrorRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");
            if (formattedDate == "1900-01-01")
            {
                formattedDate = "9999-12-28";
            }

            if (validationErrorRequest.ValidationErrorType == "Errors")
            {
                queryString = @"select * from  [dbo].[Validation_Schools_RP_GridMain_Error](@DistrictRefId,@ReportingPeriod,@CloseDate,@SchoolList)
                                union
                                select * from  [dbo].[Validation_Schools_RP_GridMain_NonFatalError](@DistrictRefId,@ReportingPeriod,@CloseDate,@SchoolList)";
            }
            else if (validationErrorRequest.ValidationErrorType == "Warnings")
            {
                queryString = "select * from  [dbo].[Validation_Schools_RP_GridMain_Warning](@DistrictRefId,@ReportingPeriod,@CloseDate,@SchoolList)";
            }
            else
            {
                queryString = "select * from  [dbo].[Validation_Schools_RP_GridMain](@DistrictRefId,@ReportingPeriod,@CloseDate,@SchoolList)";
            }

            var param = new DynamicParameters();
            param.Add("@DistrictRefID", validationErrorRequest.DistrictRefId);
            param.Add("@RP", validationErrorRequest.ReprotingPeriodName);
            param.Add("@CloseDate", formattedDate);
            param.Add("@SchoolList", validationErrorRequest.SchoolList);

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                dynamic listValidationError = con.Read().AsList();
                return listValidationError;
            }
        }

        public async Task<dynamic> GetValidationErrorSchoolGridMain(ValidationErrorRequest validationErrorRequest)
        {
            string queryString = "";
            string closeDate = validationErrorRequest.CloseDate.ToString();
            DateTime date = DateTime.Parse(closeDate);
            string formattedDate = date.ToString("yyyy-MM-dd");
            if (formattedDate == "1900-01-01")
            {
                formattedDate = "9999-12-28";
            }

            if (validationErrorRequest.ValidationErrorType == "Errors")
            {
                queryString = @"select * from  [dbo].[Validation_Schools_RP_GridMain_Error](@DistrictRefId,@ReportingPeriod,@CloseDate,@SchoolList)
                                union
                                select * from  [dbo].[Validation_Schools_RP_GridMain_NonFatalError](@DistrictRefId,@ReportingPeriod,@CloseDate,@SchoolList)";
            }
            else if (validationErrorRequest.ValidationErrorType == "Warnings")
            {
                queryString = "select * from  [dbo].[Validation_Schools_RP_GridMain_Warning](@DistrictRefId,@ReportingPeriod,@CloseDate,@SchoolList)";
            }
            else
            {
                queryString = "select * from  [dbo].[Validation_Schools_RP_GridMain](@DistrictRefId,@ReportingPeriod,@CloseDate,@SchoolList)";
            }

            var param = new DynamicParameters();
            param.Add("@DistrictRefID", validationErrorRequest.DistrictRefId);
            param.Add("@ReportingPeriod", validationErrorRequest.ReprotingPeriodName);
            param.Add("@CloseDate", formattedDate);
            param.Add("@SchoolList", validationErrorRequest.SchoolList);

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                dynamic listValidationError = con.Read().AsList();
                return listValidationError;
            }
        }

        public async Task<List<SchoolControl>> GetSchoolListByDistrictRefId(string districtId)
        {
            string queryString = "";
            queryString = "select SchoolName, SchoolId, SchoolRefId from [dbo].[Validation_Get_SchoolList_By_DistrictRefId](@DistrictRefId)";

            var param = new DynamicParameters();
            param.Add("@DistrictRefId", districtId);

            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                List<SchoolControl> listValidationError = con.ReadAsync<SchoolControl>().Result.ToList();
                return listValidationError;
            }
        }
    }
}
