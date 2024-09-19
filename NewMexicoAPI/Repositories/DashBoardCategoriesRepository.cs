using Dapper;
using Microsoft.AspNetCore.Mvc;
using NewMexicoAPI.Common;
using NewMexicoAPI.Models;
using System.Data;
using System.Security.Cryptography;

namespace NewMexicoAPI.Repositories
{
    public class DashBoardCategoriesRepository
    {
        private readonly DapperContext _dapperContext;

        public DashBoardCategoriesRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<ValidationDashboard_CategoriesList>> GetAllValidationDashboardCategories()
        {
            string queryString = "select * from [dbo].[ValidationDashboard_categoriesList]()";
            using (var connection = _dapperContext.CreateConnection_xDValidator_EdFi())
            {
                var con = await connection.QueryMultipleAsync(queryString, commandType: System.Data.CommandType.Text);
                var listValidationDashboard_CategoriesList = con.Read<ValidationDashboard_CategoriesList>().AsList();
                return listValidationDashboard_CategoriesList;
            }
        }

        public async Task Validation_TrackLogins(ValidationTrackLoginsModel model)
        {
            string queryString = "INSERT INTO dbo.Validation_TrackLogins(UserID,FirstName,LastName,OrgId,Role,LoginType,AccessProfile) VALUES (@UserID,@FirstName,@LastName,@OrgId,@Role,@LoginType,@AccessProfile)";
            using (var connection = _dapperContext.CreateConnection_EDFI_ODS_Reports())
            {
                    var TrackLogins = await connection.ExecuteAsync(queryString, model);
                    return;
            }
        }
    }
}
