using Dapper;
using NewMexicoAPI.Common;
using NewMexicoAPI.Models;

namespace NewMexicoAPI.Repositories
{
    public class ReportingPeriodRepository
    {
        private readonly DapperContext _dapperContext;

        public ReportingPeriodRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<ReportingPeriod>> GetAllReportingPeriods()
        {
            string queryString = "select * from [dbo].[ValidationDashboard_ReportingPeriodsList]() order by sortorder_UI";
            var param = new DynamicParameters();
            using (var connection = _dapperContext.CreateConnection_xDValidator_EdFi())
            {
                var con = await connection.QueryMultipleAsync(queryString, commandType: System.Data.CommandType.Text);
                var listReportingPeriodListResponse = con.Read<ReportingPeriod>().AsList();
                return listReportingPeriodListResponse;
            }
        }
    }
}
