using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewMexicoAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Data.SqlClient;
using System.Net;

namespace NewMexicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LEAErrorReportsController : Controller
    {
        [HttpGet("Get_District_RP_GridMain")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LEAErrorReportListResponse))]
        public ActionResult Get_District_RP_GridMain(string LEAInfoRefId, string ReportingPeriodName)
        {
            LEAErrorReportListResponse LEAErrorReportList = new LEAErrorReportListResponse();
            LEAErrorReportList = GetDistrictRPGridMain(LEAInfoRefId, ReportingPeriodName);
            return Json(LEAErrorReportList);
        }

        private LEAErrorReportListResponse GetDistrictRPGridMain(string LEAInfoRefId, string ReportingPeriodName)
        {
            LEAErrorReportListResponse LEAErrorReportList = new LEAErrorReportListResponse();
            string queryString = "select * from [dbo].[Validation_EDFI_District_RP_GridMain]('" + LEAInfoRefId + "','" + ReportingPeriodName + "')";
            using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection_EDFI_ODS_Reports())
            {
                SqlCommand command2 = new SqlCommand(queryString, connection2);
                connection2.Open();

                SqlDataReader reader = command2.ExecuteReader();
                try
                {
                    LEAErrorReportList.ListOfLEAErrorReport = new List<LEAErrorReportData>();
                    while (reader.Read())
                    {
                        LEAErrorReportData lEAErrorReportData = new LEAErrorReportData()
                        {
                            uid = Convert.ToInt16(reader["uid"]),
                            RefId = reader["RefId"].ToString(),
                            ObjectType = reader["ObjectType"].ToString(),
                            StateProvinceId = reader["StateProvinceId"].ToString(),
                            ErrorNumber = reader["ErrorNumber"].ToString(),
                            ErrorDesc = reader["ErrorDesc"].ToString(),
                            ErrorType = reader["ErrorType"].ToString(),
                            ValidationLevel = Convert.ToInt16(reader["ValidationLevel"]),
                            actualVals = reader["actualVals"].ToString(),
                            rName = reader["rName"].ToString(),
                            ErrorGroup = reader["ErrorGroup"].ToString(),
                            DistrictName = reader["DistrictName"].ToString(),
                            SchoolName = reader["SchoolName"].ToString(),
                            pCodeMsg = reader["pCodeMsg"].ToString(),
                            ZoneTag = reader["ZoneTag"].ToString(),
                            LEAInfoRefId = reader["LEAInfoRefId"].ToString(),
                            SchoolInfoRefId = reader["SchoolInfoRefId"].ToString(),
                            StudentPersonalRefId = reader["StudentPersonalRefId"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        };

                        LEAErrorReportList.ListOfLEAErrorReport.Add(lEAErrorReportData);
                    }
                }
                finally
                {
                    reader.Close();// Always call Close when done reading.
                    connection2.Close();
                }
            }
            return LEAErrorReportList;
        }

        [HttpGet("Get_District_RP_GridMain_Errors")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LEAErrorReportListResponse))]
        public ActionResult Get_District_RP_GridMain_Errors(string LEAInfoRefId, string ReportingPeriodName)
        {
            LEAErrorReportListResponse LEAErrorReportList = new LEAErrorReportListResponse();
            LEAErrorReportList = GetDistrictRPGridMainErrors(LEAInfoRefId, ReportingPeriodName);
            return Json(LEAErrorReportList);
        }

        private LEAErrorReportListResponse GetDistrictRPGridMainErrors(string LEAInfoRefId, string ReportingPeriodName)
        {
            LEAErrorReportListResponse LEAErrorReportList = new LEAErrorReportListResponse();
            string queryString = "select * from [dbo].[Validation_EDFI_District_RP_GridMain_Error]('" + LEAInfoRefId + "','" + ReportingPeriodName + "')";
            using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection_EDFI_ODS_Reports())
            {
                SqlCommand command2 = new SqlCommand(queryString, connection2);
                connection2.Open();

                SqlDataReader reader = command2.ExecuteReader();
                try
                {
                    LEAErrorReportList.ListOfLEAErrorReport = new List<LEAErrorReportData>();
                    while (reader.Read())
                    {
                        LEAErrorReportData lEAErrorReportData = new LEAErrorReportData()
                        {
                            uid = Convert.ToInt16(reader["uid"]),
                            RefId = reader["RefId"].ToString(),
                            ObjectType = reader["ObjectType"].ToString(),
                            StateProvinceId = reader["StateProvinceId"].ToString(),
                            ErrorNumber = reader["ErrorNumber"].ToString(),
                            ErrorDesc = reader["ErrorDesc"].ToString(),
                            ErrorType = reader["ErrorType"].ToString(),
                            ValidationLevel = Convert.ToInt16(reader["ValidationLevel"]),
                            actualVals = reader["actualVals"].ToString(),
                            rName = reader["rName"].ToString(),
                            ErrorGroup = reader["ErrorGroup"].ToString(),
                            DistrictName = reader["DistrictName"].ToString(),
                            SchoolName = reader["SchoolName"].ToString(),
                            pCodeMsg = reader["pCodeMsg"].ToString(),
                            ZoneTag = reader["ZoneTag"].ToString(),
                            LEAInfoRefId = reader["LEAInfoRefId"].ToString(),
                            SchoolInfoRefId = reader["SchoolInfoRefId"].ToString(),
                            StudentPersonalRefId = reader["StudentPersonalRefId"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        };

                        LEAErrorReportList.ListOfLEAErrorReport.Add(lEAErrorReportData);
                    }
                }
                finally
                {
                    reader.Close();// Always call Close when done reading.
                    connection2.Close();
                }
            }
            return LEAErrorReportList;
        }

        [HttpGet("Get_District_RP_GridMain_NonFatalErrors")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LEAErrorReportListResponse))]
        public ActionResult Get_District_RP_GridMain_NonFatalErrors(string LEAInfoRefId, string ReportingPeriodName)
        {
            LEAErrorReportListResponse LEAErrorReportList = new LEAErrorReportListResponse();
            LEAErrorReportList = GetDistrictRPGridMainNonFatalErrors(LEAInfoRefId, ReportingPeriodName);
            return Json(LEAErrorReportList);
        }

        private LEAErrorReportListResponse GetDistrictRPGridMainNonFatalErrors(string LEAInfoRefId, string ReportingPeriodName)
        {
            LEAErrorReportListResponse LEAErrorReportList = new LEAErrorReportListResponse();
            string queryString = "select * from [dbo].[Validation_EDFI_District_RP_GridMain_NonFatalError]('" + LEAInfoRefId + "','" + ReportingPeriodName + "')";
            using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection_EDFI_ODS_Reports())
            {
                SqlCommand command2 = new SqlCommand(queryString, connection2);
                connection2.Open();

                SqlDataReader reader = command2.ExecuteReader();
                try
                {
                    LEAErrorReportList.ListOfLEAErrorReport = new List<LEAErrorReportData>();
                    while (reader.Read())
                    {
                        LEAErrorReportData lEAErrorReportData = new LEAErrorReportData()
                        {
                            uid = Convert.ToInt16(reader["uid"]),
                            RefId = reader["RefId"].ToString(),
                            ObjectType = reader["ObjectType"].ToString(),
                            StateProvinceId = reader["StateProvinceId"].ToString(),
                            ErrorNumber = reader["ErrorNumber"].ToString(),
                            ErrorDesc = reader["ErrorDesc"].ToString(),
                            ErrorType = reader["ErrorType"].ToString(),
                            ValidationLevel = Convert.ToInt16(reader["ValidationLevel"]),
                            actualVals = reader["actualVals"].ToString(),
                            rName = reader["rName"].ToString(),
                            ErrorGroup = reader["ErrorGroup"].ToString(),
                            DistrictName = reader["DistrictName"].ToString(),
                            SchoolName = reader["SchoolName"].ToString(),
                            pCodeMsg = reader["pCodeMsg"].ToString(),
                            ZoneTag = reader["ZoneTag"].ToString(),
                            LEAInfoRefId = reader["LEAInfoRefId"].ToString(),
                            SchoolInfoRefId = reader["SchoolInfoRefId"].ToString(),
                            StudentPersonalRefId = reader["StudentPersonalRefId"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        };

                        LEAErrorReportList.ListOfLEAErrorReport.Add(lEAErrorReportData);
                    }
                }
                finally
                {
                    reader.Close();// Always call Close when done reading.
                    connection2.Close();
                }
            }
            return LEAErrorReportList;
        }

        [HttpGet("Get_District_RP_GridMain_Warnings")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LEAErrorReportListResponse))]
        public ActionResult Get_District_RP_GridMain_Warnings(string LEAInfoRefId, string ReportingPeriodName)
        {
            LEAErrorReportListResponse LEAErrorReportList = new LEAErrorReportListResponse();
            LEAErrorReportList = GetDistrictRPGridMainWarnings(LEAInfoRefId, ReportingPeriodName);
            return Json(LEAErrorReportList);
        }

        private LEAErrorReportListResponse GetDistrictRPGridMainWarnings(string LEAInfoRefId, string ReportingPeriodName)
        {
            LEAErrorReportListResponse LEAErrorReportList = new LEAErrorReportListResponse();
            string queryString = "select * from [dbo].[Validation_EDFI_District_RP_GridMain_Warning]('" + LEAInfoRefId + "','" + ReportingPeriodName + "')";
            using (SqlConnection connection2 = NewMexicoAPI.Common.DBConnection.GetDBConnection_EDFI_ODS_Reports())
            {
                SqlCommand command2 = new SqlCommand(queryString, connection2);
                connection2.Open();

                SqlDataReader reader = command2.ExecuteReader();
                try
                {
                    LEAErrorReportList.ListOfLEAErrorReport = new List<LEAErrorReportData>();
                    while (reader.Read())
                    {
                        LEAErrorReportData lEAErrorReportData = new LEAErrorReportData()
                        {
                            uid = Convert.ToInt16(reader["uid"]),
                            RefId = reader["RefId"].ToString(),
                            ObjectType = reader["ObjectType"].ToString(),
                            StateProvinceId = reader["StateProvinceId"].ToString(),
                            ErrorNumber = reader["ErrorNumber"].ToString(),
                            ErrorDesc = reader["ErrorDesc"].ToString(),
                            ErrorType = reader["ErrorType"].ToString(),
                            ValidationLevel = Convert.ToInt16(reader["ValidationLevel"]),
                            actualVals = reader["actualVals"].ToString(),
                            rName = reader["rName"].ToString(),
                            ErrorGroup = reader["ErrorGroup"].ToString(),
                            DistrictName = reader["DistrictName"].ToString(),
                            SchoolName = reader["SchoolName"].ToString(),
                            pCodeMsg = reader["pCodeMsg"].ToString(),
                            ZoneTag = reader["ZoneTag"].ToString(),
                            LEAInfoRefId = reader["LEAInfoRefId"].ToString(),
                            SchoolInfoRefId = reader["SchoolInfoRefId"].ToString(),
                            StudentPersonalRefId = reader["StudentPersonalRefId"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        };

                        LEAErrorReportList.ListOfLEAErrorReport.Add(lEAErrorReportData);
                    }
                }
                finally
                {
                    reader.Close();// Always call Close when done reading.
                    connection2.Close();
                }
            }
            return LEAErrorReportList;
        }
    }
}
