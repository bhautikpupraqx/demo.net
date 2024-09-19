using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewMexicoAPI.Models;
using NewMexicoAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace NewMexicoAPI.Controllers
{
    [RequireHttps]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;


        public AuthController(IConfiguration configuration, IUserService userService, ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _userService = userService;
            _logger = logger;

        }

        [AllowAnonymous]
        [HttpPost(nameof(Auth))]
        public IActionResult Auth([FromBody] LoginModel data)
        {
            try
            {
                _logger.LogInformation("Auth Method Called");
                bool isValid = _userService.IsValidUserInformation(data);
                if (isValid)
                {
                    var configurationDetails = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    int tokenExpirationSeconds = configurationDetails.GetValue<int>("TokenExpirationSeconds");
                    DateTime issuedDate = DateTime.UtcNow;
                    DateTime expirationDate = DateTime.UtcNow.AddSeconds(tokenExpirationSeconds);
                    var tokenString = GenerateJwtToken(data.UserName, tokenExpirationSeconds);

                    AuthTokenData authTokenData = new AuthTokenData()
                    {
                        access_token = tokenString,
                        token_type = "bearer",
                        expires_in = tokenExpirationSeconds,
                        userName = data.UserName,
                        issuedDate = issuedDate,
                        expiresDate = expirationDate
                    };
                    _logger.LogInformation("Log Error for auth API:");
                    return Ok(new { Token = authTokenData, Message = "Success" });
                }
                _logger.LogInformation("Log Error for auth API:");

                return BadRequest("Please pass the valid Username and Password");
            }
            catch (Exception ex)
            {
                _logger.LogError("Log Error for auth API: " + ex.Message, "Auth Error");

                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetUserData))]
        public IActionResult GetUserData()
        {

            var loginModel = _userService.GetUserDetails();
            return Ok(new { User = loginModel, Message = "API Validated" });
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(nameof(AddUserDetails))]
        public IActionResult AddUserDetails(string username, string password)
        {
            bool result = _userService.AddUserDetails(username, password);
            if (result)
            {
                return Ok(new { Message = "User Added Successfully" });
            }
            else
            {
                return BadRequest(new { Message = "User already exist" });
            }
        }

        /// <summary>
        /// Generate JWT Token after successful login.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private string GenerateJwtToken(string userName, int tokenExpirationSeconds)
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
    }
}
