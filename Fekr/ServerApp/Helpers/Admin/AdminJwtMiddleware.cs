using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerApp.Services;

namespace ServerApp.Helpers.Admin
{
    public class AdminJwtMiddleware
    {
        private readonly AppSettings _appSettings;
        private readonly RequestDelegate _next;

        public AdminJwtMiddleware(
            RequestDelegate next,
            IOptions<AppSettings> appSettings
        )
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task
            Invoke(HttpContext context, IAdminLoginService userService)
        {
            var token =
                context
                    .Request
                    .Headers["Authorization"]
                    .FirstOrDefault()?
                    .Split(" ")
                    .Last();

            if (token != null) attachUserToContext(context, userService, token);

            await _next(context);
        }

        private void attachUserToContext(
            HttpContext context,
            IAdminLoginService userService,
            string token
        )
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler
                    .ValidateToken(token,
                        new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                            ClockSkew = TimeSpan.Zero
                        },
                        out var validatedToken);

                var jwtToken = (JwtSecurityToken) validatedToken;

                //for id of type int
                //var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                //for id of type string
                var userId =
                    jwtToken.Claims.First(x => x.Type == "id").Value.ToString();

                // attach user to context on successful jwt validation
                context.Items["Admin"] = userService.GetById(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}