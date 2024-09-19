using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewMexicoAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Data.SqlClient;
using System.Net;

namespace NewMexicoAPI.Controllers
{
    [RequireHttps]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class LEASchoolController : Controller
    {
        [HttpGet("Get_SchoolList_By_DistrictRefId")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LEASchoolListResponse))]
        public ActionResult Get_SchoolList_By_DistrictRefId(string LEAInfoRefId)
        {
            LEASchoolListResponse lstLEASchools = new LEASchoolListResponse();
            lstLEASchools = GetSchoolListByDistrictRefId(LEAInfoRefId);
            return Json(lstLEASchools);
        }

        private LEASchoolListResponse GetSchoolListByDistrictRefId(string LEAInfoRefId)
        {
            LEASchoolListResponse lstLEASchools = new LEASchoolListResponse();
            string queryString = "select * from [dbo].[Validation_EDFI_Get_SchoolList_By_DistrictRefId]('" + LEAInfoRefId + "') order by SchoolName";
            using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection_EDFI_ODS_Reports())
            {
                SqlCommand command2 = new SqlCommand(queryString, connection2);
                connection2.Open();

                SqlDataReader reader = command2.ExecuteReader();
                try
                {
                    lstLEASchools.ListOfLEASchool = new List<LEASchool>();
                    while (reader.Read())
                    {
                        LEASchool lEASchool = new LEASchool();
                        lEASchool.SchoolName = reader["SchoolName"].ToString();
                        lEASchool.RefId = reader["RefId"].ToString();

                        lstLEASchools.ListOfLEASchool.Add(lEASchool);
                    }
                }
                finally
                {
                    reader.Close();// Always call Close when done reading.
                    connection2.Close();
                }
            }
            return lstLEASchools;
        }

        [HttpGet("Get_SchoolRefID_By_DistrictRefId")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LEARefIDResponse))]
        public ActionResult Get_SchoolRefID_By_DistrictRefId(string LEAInfoRefId)
        {
            LEARefIDResponse lstLEAControlRefId = new LEARefIDResponse();
            lstLEAControlRefId = GetSchoolRefIDByDistrictRefId(LEAInfoRefId);
            return Json(lstLEAControlRefId);
        }

        private LEARefIDResponse GetSchoolRefIDByDistrictRefId(string LEAInfoRefId)
        {
            LEARefIDResponse lstLEARefId = new LEARefIDResponse();
            string queryString = "select * from [dbo].[Validation_EDFI_Get_SchoolRefID_By_DistrictRefId]('" + LEAInfoRefId + "')";
            using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection_EDFI_ODS_Reports())
            {
                SqlCommand command2 = new SqlCommand(queryString, connection2);
                connection2.Open();

                SqlDataReader reader = command2.ExecuteReader();
                try
                {
                    lstLEARefId.lstLEARefID = new List<LEARefID>();
                    while (reader.Read())
                    {
                        LEARefID lEARefID = new LEARefID();
                        lEARefID.RefId = reader["RefId"].ToString();

                        lstLEARefId.lstLEARefID.Add(lEARefID);
                    }
                }
                finally
                {
                    reader.Close();// Always call Close when done reading.
                    connection2.Close();
                }
            }
            return lstLEARefId;
        }

        [HttpGet("Get_SchoolName_By_SchoolRefId")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(SchoolNameResponse))]
        public ActionResult Get_SchoolName_By_SchoolRefId(string SchoolInfoRefId)
        {
            SchoolNameResponse schoolNameResponse = new SchoolNameResponse();
            schoolNameResponse = GetSchoolNameBySchoolRefId(SchoolInfoRefId);
            return Json(schoolNameResponse);
        }

        private SchoolNameResponse GetSchoolNameBySchoolRefId(string SchoolInfoRefId)
        {
            SchoolNameResponse schoolNameResponse = new SchoolNameResponse();
            string queryString = "select * from [dbo].[Validation_EDFI_Get_SchoolName_By_SchoolRefId]('" + SchoolInfoRefId + "')";
            using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection_EDFI_ODS_Reports())
            {
                SqlCommand command2 = new SqlCommand(queryString, connection2);
                connection2.Open();

                SqlDataReader reader = command2.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        schoolNameResponse.SchoolName = reader["SchoolName"].ToString();
                    }
                }
                finally
                {
                    reader.Close();// Always call Close when done reading.
                    connection2.Close();
                }
            }
            return schoolNameResponse;
        }
    }
}
