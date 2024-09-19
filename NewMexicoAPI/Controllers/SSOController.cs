using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewMexicoAPI.Models;
using NewMexicoAPI.Repositories;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace NewMexicoAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class SSOController : ControllerBase
    {
        private readonly ILogger<SSOController> _logger;
        private readonly IConfiguration _configuration;
        string SSOXMLFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/XML/SSOXML/SSOXML.xml");
        private readonly DistrictHomeRepository _districtHomeRepository;
        private readonly ValidationErrorRepository _validationErrorRepository;
        private readonly DashBoardCategoriesRepository _dashBoardCategoriesRepository;
        public SSOController(ILogger<SSOController> logger, IConfiguration configuration,
            DistrictHomeRepository districtHomeRepository, ValidationErrorRepository validationErrorRepository, DashBoardCategoriesRepository dashBoardCategoriesRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _districtHomeRepository = districtHomeRepository;
            _validationErrorRepository = validationErrorRepository;
            _dashBoardCategoriesRepository = dashBoardCategoriesRepository;
        }

        [HttpGet("SSO")]
        public async Task<ActionResult> SSO()
        {
            #region Declaration
            ValidationTrackLoginsModel TrackLoginsModel = new ValidationTrackLoginsModel();
            string responseStr = "";
            XElement SSOXML = null;
            string bearerToken = "";
            string userAppAccess = "";
            XElement xUserAppAccess = null;
            string postErrMsg = string.Empty;
            string profileXml = string.Empty;
            
            string staffrecordId = string.Empty;
            string FirstName = string.Empty;
            string LastName = string.Empty;
            string Email = string.Empty;
            string RoleName = string.Empty;
            List<string> listDistrictCodes;
            List<string> listSchoolCodes;
            string districtCodes = string.Empty;
            string schoolCodes = string.Empty;
            string districtName = string.Empty;
            #endregion Declaration

            try
            {
                #region Read the SSO Setup XML

                var ApplicationID = _configuration["newMaxicoSSOConfig:ApplicationIDP_Assigned_ID"];
                var SSOXMLFromFile = _configuration["newMaxicoSSOConfig:SSOXMLFromFile"];
                var ClientID = _configuration["newMaxicoSSOConfig:ClientID"];
                var ClientSecret = _configuration["newMaxicoSSOConfig:ClientSecret"];
                var BaseURL = _configuration["newMaxicoSSOConfig:BaseURL"];

                #endregion End Read the SSO Setup XML 

                //string samlRequest = HttpContext.Request.Query["SAMLRequest"];
                string samlRequest = "?SAMLRequest=123";//HttpContext.Request.Query["SAMLRequest"];
                _logger.LogInformation("***SAMLRequest string is = " + samlRequest);
                if (samlRequest == null)
                {
                    // no access
                    return Ok("Access Denied");
                }
                #region SSOXMLFromFile = true
                if ((SSOXMLFromFile ?? "").ToLower() == "true")
                {
                    responseStr = XElement.Load(SSOXMLFile).ToString();
                    if (!string.IsNullOrEmpty(responseStr))
                    {
                        SSOXML = XElement.Parse(responseStr, LoadOptions.PreserveWhitespace);
                        _logger.LogInformation("***SSOXML profile node xml data is: " + SSOXML.ToString());
                    }
                }
                #endregion

                #region SSOXMLFromFile = false
                if ((SSOXMLFromFile ?? "").ToLower() == "false")
                {

                    _logger.LogInformation("***Caling GetBearerToken");
                    bearerToken = GetBearerToken(BaseURL, ClientID, ClientSecret);
                    HttpContext.Session.SetString("Token", bearerToken);
                    _logger.LogInformation("***bearerToken is: " + bearerToken);

                    if (bearerToken != null)
                    {
                        //samlRequest = "LrcEJOL9Bv607XwSX9EjiPf4DTfOQrEil9L9SXdXtXm7YwKjnYtGqhS7mibZJ7sk1WY8ly005QZuCjfhj7A3893eqPVKWooxFcfbc2178ab2-8e3f-4faf-bf24-20cc82249c27";
                        XElement xUserAppAccessRequest = new XElement("UserAppAccess_Attribute",
                                                         new XElement("ApplicationIDP_Assigned_ID", ApplicationID),
                                                         new XElement("UserToken", samlRequest));

                        _logger.LogInformation("***xUserAppAccessRequest Payload is = " + xUserAppAccessRequest);

                        try
                        {
                            _logger.LogInformation("***Caling PostFromRest");
                            userAppAccess = PostFromRest(BaseURL, "api/UserAppAccess_Attribute", bearerToken, xUserAppAccessRequest);
                            _logger.LogInformation("***userAppAccess is: " + userAppAccess);
                            if (string.IsNullOrEmpty(userAppAccess))
                            {
                                //"Error getting response string";
                                _logger.LogError("-------------------------------Error-------------------------------");
                                _logger.LogError("Error getting response string is :" + userAppAccess);
                                _logger.LogError("--------------------------------------------------------------");
                            }
                            try
                            {
                                xUserAppAccess = XElement.Parse(userAppAccess);
                                if (xUserAppAccess?.Element("Error") != null)
                                {
                                    postErrMsg = xUserAppAccess.Element("Error").Value.ToString();
                                    _logger.LogError("xUserAppAccess Error is :" + postErrMsg);
                                    if (postErrMsg == "UserToken does not exist")
                                    {
                                        return Ok("Access Denied");
                                    }
                                }
                                else
                                {
                                    profileXml = xUserAppAccess.ToString().Replace("\t", " ");
                                    _logger.LogInformation("***profileXml is = " + profileXml);

                                    SSOXML = XElement.Parse(profileXml);
                                    _logger.LogInformation("***SSOXML is = " + SSOXML);
                                }

                            }
                            catch (Exception ex)
                            {
                                _logger.LogError("-------------------------------Error-------------------------------");
                                _logger.LogError("isXML is :" + ex.ToString());
                                _logger.LogError("isXML values error is :" + ex.Message);
                                _logger.LogError("--------------------------------------------------------------");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("-------------------------------Error-------------------------------");
                            _logger.LogError("isXML is :" + ex.ToString());
                            _logger.LogError("isXML values error is :" + ex.Message);
                            _logger.LogError("--------------------------------------------------------------");
                        }
                    }
                    else
                    {
                        //access denied
                        _logger.LogError("-------------------------------Error-------------------------------");
                        _logger.LogError("access denied");
                        _logger.LogError("--------------------------------------------------------------");
                        return Ok("Access Denied");
                    }
                }
                #endregion

                if (SSOXML != null)
                {
                    _logger.LogInformation("***SSOXML=" + SSOXML.ToString());

                    string UserID = SSOXML.Element("UserId").Value;
                    TrackLoginsModel.UserID = UserID;

                    XElement xAppProfile = SSOXML.Element("AppProfile")!;
                    TrackLoginsModel.AccessProfile = xAppProfile.ToString();
                    _logger.LogInformation("***xAppProfile is =" + xAppProfile.ToString());


                    staffrecordId = xAppProfile.Element("staffrecordId")?.Value ?? "";
                    _logger.LogInformation("***staffrecordId is =" + staffrecordId.ToString());

                    FirstName = xAppProfile.Element("FirstName")?.Value ?? "";
                    TrackLoginsModel.FirstName = FirstName;
                    _logger.LogInformation("***FirstName is =" + FirstName.ToString());

                    LastName = xAppProfile.Element("LastName")?.Value ?? "";
                    TrackLoginsModel.LastName = LastName;
                    _logger.LogInformation("***LastName is =" + LastName.ToString());


                    Email = xAppProfile.Element("Email")?.Value ?? "";
                    _logger.LogInformation("***Email is =" + Email.ToString());

                    //Roles
                    if (string.IsNullOrEmpty(ApplicationID))
                    {
                        //return "Error: ApplicationIDP_Assigned_ID not set in appsettings.json";
                        _logger.LogError("-------------------------------Error-------------------------------");
                        _logger.LogError("ApplicationIDP_Assigned_ID not set in appsettings.json");
                        _logger.LogError("--------------------------------------------------------------");
                        return Ok("Access Denied");
                    }

                    XElement Roles = xAppProfile.Elements("Application").FirstOrDefault(n => n.Attribute("Id").Value == ApplicationID).Element("Roles");
                    if (Roles == null || !Roles.Elements().Any())
                    {
                        //return "Error getting roles";
                        _logger.LogError("-------------------------------Error-------------------------------");
                        _logger.LogError("Error getting roles");
                        _logger.LogError("--------------------------------------------------------------");
                        return Ok("Access Denied");
                    }
                    else
                    {
                        XElement Role = Roles.Elements().Where(w => !string.IsNullOrEmpty(w.Attribute("Id").Value)).FirstOrDefault();

                        RoleName = Role.Attribute("Name").Value.ToLower() ?? "";
                        TrackLoginsModel.Role = RoleName;
                        _logger.LogInformation("***Roles Name =" + RoleName);

                        listDistrictCodes = Role.Elements().Where((s => s.Attribute("TypeCode") != null && s.Attribute("TypeCode").Value == "01")).Select(n => n.Attribute("Code").Value).ToList() ?? new List<string>();
                        _logger.LogInformation("***listDistrictCodes is =" + listDistrictCodes.ToString());

                        listSchoolCodes = Role.Elements().Where((s => s.Attribute("TypeCode") != null && s.Attribute("TypeCode").Value == "02")).Select(n => n.Attribute("Code").Value).ToList() ?? new List<string>();
                        _logger.LogInformation("***Roles Name =" + listDistrictCodes.ToString());


                        districtName = Role.Elements().Where((s => s.Attribute("TypeCode") != null && s.Attribute("TypeCode").Value == "01")).Select(n => n.Attribute("Name").Value).FirstOrDefault();
                    }

                    if (RoleName == "district" || RoleName == "districtschool")
                    {
                        _logger.LogInformation("***Roles Name =" + RoleName);
                        districtCodes = string.Join(",", listDistrictCodes.Select(a => a ?? "").ToList());
                        TrackLoginsModel.OrgId = districtCodes;
                        _logger.LogInformation("***districtCodes is =" + districtCodes);

                        if (listSchoolCodes.Count == 0)
                        {
                            schoolCodes = "Allschools";
                            _logger.LogInformation("***schoolCodes is =" + schoolCodes);
                        }
                        else
                        {
                            schoolCodes = string.Join(",", listSchoolCodes.Select(a => a ?? "").ToList());
                            _logger.LogInformation("***schoolCodes is =" + schoolCodes);
                        }
                    }

                    if (RoleName == "school")
                    {
                        _logger.LogInformation("***Roles Name =" + RoleName);
                        districtCodes = string.Join(",", listDistrictCodes.Select(a => a ?? "").ToList());
                        _logger.LogInformation("***districtCodes is =" + districtCodes);

                        schoolCodes = string.Join(",", listSchoolCodes.Select(a => a ?? "").ToList());
                        _logger.LogInformation("***schoolCodes is =" + schoolCodes);
                    }

                    if (RoleName == "state")
                    {
                        _logger.LogInformation("***Roles Name =" + RoleName);

                        districtCodes = string.Join(",", listDistrictCodes.Select(a => a ?? "").ToList());
                        _logger.LogInformation("***districtCodes is =" + districtCodes);

                        schoolCodes = "Allschools";
                        _logger.LogInformation("***schoolCodes is =" + schoolCodes);
                    }

                    string unformattedXml = @SSOXML.ToString();
                    _logger.LogInformation("***unformattedXml is =" + unformattedXml);

                    // Parse the XML string into an XDocument
                    XDocument doc = XDocument.Parse(unformattedXml);

                    // Format (pretty-print) the XML
                    string formattedXml = doc.ToString();
                    _logger.LogInformation("***formattedXml is =" + formattedXml);

                    HttpContext.Session.SetString("staffrecordId", staffrecordId);
                    HttpContext.Session.SetString("FirstName", FirstName);
                    HttpContext.Session.SetString("LastName", LastName);
                    HttpContext.Session.SetString("Email", Email);
                    HttpContext.Session.SetString("RoleName", RoleName);
                    HttpContext.Session.SetString("districtCodes", districtCodes);
                    HttpContext.Session.SetString("schoolCodes", schoolCodes);
                    HttpContext.Session.SetString("District", districtName);
                    HttpContext.Session.SetString("fromSSO", "true");

                    if ((SSOXMLFromFile ?? "").ToLower() == "true")
                    {
                        #region TOken
                        int tokenExpirationSeconds = Convert.ToInt32(_configuration["TokenExpirationSeconds"]);
                        DateTime issuedDate = DateTime.UtcNow;
                        DateTime expirationDate = DateTime.UtcNow.AddSeconds(tokenExpirationSeconds);
                        //var tokenString = SSOGenerateJwtToken(FirstName, tokenExpirationSeconds);
                        var tokenString = GetBearerToken(BaseURL, ClientID, ClientSecret);
                       // HttpContext.Session.SetString("Token", "Bearer " + tokenString);
                        HttpContext.Session.SetString("Token", tokenString);

                        #endregion
                    }


                }
            }
            catch (Exception ex)
            {

                _logger.LogError("-------------------------------Error-------------------------------");
                _logger.LogError("isXML is :" + ex.ToString());
                _logger.LogError("isXML values error is :" + ex.Message);
                _logger.LogError("--------------------------------------------------------------");
                return Ok("Access Denied");
            }

            var districtRefId = _districtHomeRepository.GetDistrictRefIdByDistrictCode(districtCodes);
            //var roleId = _districtHomeRepository.GetSSORoleRefID(UserId, districtCodes, RoleName);
            schoolCodes = HttpContext.Session.GetString("schoolCodes");
            var schoolRefIds = "";
            if (schoolCodes != "AllSchools")
            {
                List<SchoolControl> schoolList = await _validationErrorRepository.GetSchoolListByDistrictRefId(districtRefId);
                if (schoolList != null && schoolList.Count > 0)
                {
                    var schoolIdsFromAppData = schoolCodes.Split(",").ToArray();
                    schoolRefIds = string.Join(",", schoolList.Where(x => schoolIdsFromAppData.Contains(x.SchoolID.ToString())).Select(x => x.SchoolRefId).ToArray());
                }
                else
                {
                    schoolRefIds = "";
                }
            }
            TrackLoginsModel.LoginType = "SSO";


            Dictionary<string, string> SSOlist = new Dictionary<string, string>();
            SSOlist.Add("SSO_FirstName", HttpContext.Session.GetString("FirstName"));
            SSOlist.Add("SSO_LastName", HttpContext.Session.GetString("LastName"));
            SSOlist.Add("SSO_Email", HttpContext.Session.GetString("Email"));
            SSOlist.Add("UserName", HttpContext.Session.GetString("FirstName"));
            SSOlist.Add("District", HttpContext.Session.GetString("District"));
            SSOlist.Add("DistrictId", HttpContext.Session.GetString("districtCodes"));
            SSOlist.Add("DistrictRefId", districtRefId);
            SSOlist.Add("UserId", HttpContext.Session.GetString("UserId"));
            SSOlist.Add("Role", HttpContext.Session.GetString("RoleName"));
            SSOlist.Add("Schools", HttpContext.Session.GetString("schoolCodes"));
            SSOlist.Add("SchoolRefIds", schoolRefIds);
            SSOlist.Add("fromSSO", HttpContext.Session.GetString("fromSSO"));
            SSOlist.Add("Token", HttpContext.Session.GetString("Token"));

            _dashBoardCategoriesRepository.Validation_TrackLogins(TrackLoginsModel);

            return Ok(SSOlist);
        }
        private string SSOGenerateJwtToken(string userName, int tokenExpirationSeconds)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userName) }),
                Expires = DateTime.UtcNow.AddSeconds(tokenExpirationSeconds),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var configurationDetails = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string pattern = configurationDetails.GetValue<string>("TokenPattern");
            Match m = Regex.Match(((JwtSecurityToken)token).RawData.ToString(), pattern, RegexOptions.IgnoreCase);

            if (m.Success)
                return tokenHandler.WriteToken(token);
            else
                return "Token not generated as it was not matched with Regex, please try again.";
        }

        string GetBearerToken(string API_URL, string ClientID, string ClientSecret)
        {
            string access_token = "";
            string URL = API_URL;
            string TokenURL = URL + "token";
            string Token_Param = string.Format("grant_type=password&username={0}&password={1}",
          ClientID, HttpUtility.UrlEncode(ClientSecret));
            RestClient client = null;
            try
            {
                client = new RestClient(TokenURL);
            }
            catch (Exception ex)
            {
                //cLogger.WriteInfo(" 1a GetBearerToken error creating new RestClient(TokenURL)=\n" +
                //ex.Message);
            }
            RestRequest request = null;
            try { request = new RestRequest("", Method.Post); }
            catch (Exception ex1)
            {
                //cLogger.WriteInfo(" 1a GetBearerToken error creating new RestRequest=\n" +
                //ex1.Message);
            }
            request.AddHeader("cache-control", "no-cache");
            //   request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", Token_Param.ToString(),
                            ParameterType.RequestBody);
            string Token_BearerToken = "";
            try
            {
                RestResponse response = client.Execute(request);
                string responseStatus = response.StatusCode.ToString();
                if (responseStatus == "OK") { Token_BearerToken = response.Content.ToString(); }
            }
            catch (Exception ex1)
            {
                //cLogger.WriteInfo("Error:\n" + ex1.Message ?? "");
            }
            if (Token_BearerToken.Length > 0)
                try
                {
                    access_token = (string)JsonNode.Parse(Token_BearerToken)["access_token"];
                }
                catch (Exception ex)
                {
                    access_token = "";
                    //cLogger.WriteInfo("Error in JsonNode.Parse(Token_BearerToken:\n" + ex.Message);
                }
            string BearerToken = "";
            if (!string.IsNullOrEmpty(access_token))
            {
                BearerToken = $"Bearer {access_token}";
            }
            return BearerToken;
        }
        string PostFromRest(string API_URL, string subURL, string Authorization, XElement Payload)
        {
            var client = new RestClient(API_URL);
            var request = new RestRequest(subURL, Method.Post);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/xml");
            request.AddHeader("Accept", "application/xml");
            request.AddHeader("messageType", "REQUEST");
            request.AddHeader("Authorization", Authorization);
            request.AddHeader("messageId", Guid.NewGuid().ToString());
            request.AddHeader("timestamp", DateTime.Now.ToString());
            request.AddXmlBody(Payload, "application/xml");//request.AddStringBody( Payload, ContentType.Xml);
            var response = client.Execute(request);
            return response.Content;
        }
    }
}
