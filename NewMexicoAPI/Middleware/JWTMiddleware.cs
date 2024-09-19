using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewMexicoAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NewMexicoAPI.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _configuration = configuration;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Produces("application/xml")]
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var FromSSO = context.Request.Headers["FromSSO"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                attachAccountToContext(context, token);
            }
            await _next(context);
        }

        private void attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                //var a = context.Session.GetString("fromSSO");
                //if(_httpContextAccessor.HttpContext.Session.GetString("fromSSO") != null)
                //{
                //    _httpContextAccessor.HttpContext.Items["User"] = _userService.GetUserDetails();
                //}
                //else
                //{
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var accountId = jwtToken.Claims.First(x => x.Type == "id").Value;
                    // attach account to context on successful jwt validation
                    _httpContextAccessor.HttpContext.Items["User"] = _userService.GetUserDetails();
                //}
              
            }
            catch (Exception ex)
            {
                _httpContextAccessor.HttpContext.Items["User"] = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                //throw new Exception(ex.Message);

                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }
}
