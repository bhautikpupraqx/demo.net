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
    public class LEAController : Controller
    {
        [HttpGet("Get_All_isActiveList_LEAControl")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LEAControlListResponse))]
        public ActionResult Get_All_isActiveList_LEAControl()
        {
            LEAControlListResponse lstLEAControl = new LEAControlListResponse();
            lstLEAControl = GetAllActiveLEAControls();
            return Ok(lstLEAControl);
        }

        private LEAControlListResponse GetAllActiveLEAControls()
        {
            LEAControlListResponse lstLEAControl = new LEAControlListResponse();
            string queryString = "select * from [dbo].[Validation_EDFI_Get_All_isActiveList_LEAControl]() order by Lea_Name";
            using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection_EDFI_ODS_Reports())
            {
                SqlCommand command2 = new SqlCommand(queryString, connection2);
                connection2.Open();

                SqlDataReader reader = command2.ExecuteReader();
                try
                {
                    lstLEAControl.lstLEAControls = new List<LEAControl>();
                    while (reader.Read())
                    {
                        LEAControl lEAControl = new LEAControl();
                        lEAControl.Lea_Name = reader["Lea_Name"].ToString();
                        lEAControl.RefId = reader["RefId"].ToString();

                        lstLEAControl.lstLEAControls.Add(lEAControl);
                    }
                }
                finally
                {
                    reader.Close();// Always call Close when done reading.
                    connection2.Close();
                }
            }
            return lstLEAControl;
        }

        [HttpGet("Get_All_isActive_LEAControl")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LEARefIDResponse))]
        public ActionResult Get_All_isActive_LEAControl()
        {
            LEARefIDResponse lstLEAControlRefId = new LEARefIDResponse();
            lstLEAControlRefId = GetAllActiveLEAControlRefIds();
            return Json(lstLEAControlRefId);
        }

        private LEARefIDResponse GetAllActiveLEAControlRefIds()
        {
            LEARefIDResponse lstLEAControlRefId = new LEARefIDResponse();
            string queryString = "select * from [dbo].[Validation_EDFI_Get_All_isActive_LEAControl]()";
            using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection_EDFI_ODS_Reports())
            {
                SqlCommand command2 = new SqlCommand(queryString, connection2);
                connection2.Open();

                SqlDataReader reader = command2.ExecuteReader();
                try
                {
                    lstLEAControlRefId.lstLEARefID = new List<LEARefID>();
                    while (reader.Read())
                    {
                        LEARefID lEAControlRefID = new LEARefID();
                        lEAControlRefID.RefId = reader["RefId"].ToString();

                        lstLEAControlRefId.lstLEARefID.Add(lEAControlRefID);
                    }
                }
                finally
                {
                    reader.Close();// Always call Close when done reading.
                    connection2.Close();
                }
            }
            return lstLEAControlRefId;
        }

        [HttpGet("Get_DistrictName_ByRefId")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DistrictNameResponse))]
        public ActionResult Get_DistrictName_ByRefId(string LEAInfoRefId)
        {
            DistrictNameResponse districtNameResponse = new DistrictNameResponse();
            districtNameResponse = GetDistrictNameByRefId(LEAInfoRefId);
            return Json(districtNameResponse);
        }

        private DistrictNameResponse GetDistrictNameByRefId(string LEAInfoRefId)
        {
            DistrictNameResponse districtNameResponse = new DistrictNameResponse();
            string queryString = "select * from [dbo].[Validation_EDFI_Get_DistrictName_ByRefId]('" + LEAInfoRefId + "')";
            using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection_EDFI_ODS_Reports())
            {
                SqlCommand command2 = new SqlCommand(queryString, connection2);
                connection2.Open();

                SqlDataReader reader = command2.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        districtNameResponse.Lea_Name = reader["Lea_Name"].ToString();
                    }
                }
                finally
                {
                    reader.Close();// Always call Close when done reading.
                    connection2.Close();
                }
            }
            return districtNameResponse;
        }
    }
}
