using Dapper;
using NewMexicoAPI.Common;
using NewMexicoAPI.Models;

namespace NewMexicoAPI.Repositories
{
    public class RuleExplorerRepository
    {
        private readonly DapperContext _dapperContext;

        public RuleExplorerRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<dynamic> GetRuleExplorerData(ValidationErrorRequest validationErrorRequest)
        {
            try
            {
                string queryString = "";
                var param = new DynamicParameters();

                if (validationErrorRequest.ReprotingPeriodName != "Full Year")
                {
                    queryString = "select * from [dbo].[ValidationDashboard_Items_By_ReportingPeriodName_List](@ReportingPeriod)";
                    param.Add("@ReportingPeriod", validationErrorRequest.ReprotingPeriodName);
                }
                else
                {
                    queryString = "select * from [dbo].[ValidationDashboard_Items_No_ReportingPeriod_Assigned_List]()";
                }

                using (var connection = _dapperContext.CreateConnection_xDValidator_EdFi())
                {
                    if (validationErrorRequest.ReprotingPeriodName != "Full Year")
                    {
                        var con = await connection.QueryMultipleAsync(queryString, param: param, commandType: System.Data.CommandType.Text);
                        dynamic listRuleExplorer = con.Read().AsList();
                        return listRuleExplorer;
                    }
                    else
                    {
                        var con = await connection.QueryMultipleAsync(queryString, commandType: System.Data.CommandType.Text);
                        dynamic listRuleExplorer = con.Read().AsList();
                        return listRuleExplorer;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
