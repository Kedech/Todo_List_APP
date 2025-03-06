using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using Todo_List_API.Helpers;
using Todo_List_API.Services.Interfaces;

namespace Todo_List_API.Middlewares
{
    public class TokenRenewalMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenRenewalMiddleware> _logger;
        private readonly static string nameAssembly = Assembly.GetExecutingAssembly().GetName().Name!;

        public TokenRenewalMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<TokenRenewalMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            var path = context.Request.Path.Value;

            //// Excluir la ruta de Swagger de la validación del token
            //if (path != null && (path.StartsWith("/swagger") || path.StartsWith("/User/")))
            //{
            //    await _next(context);
            //    return;
            //}

            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var expiryDateUnix = long.Parse(jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                    var expiryDateTimeUtc = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix).UtcDateTime;

                    if (expiryDateTimeUtc < DateTime.UtcNow)
                    {
                        var userEmail = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
                        var user = await userService.GetUserByEmailAsync(userEmail);

                        if (user != null)
                        {
                            var newToken = MethodsHelpers.GenerateJwtToken(user, _configuration);
                            context.Response.Headers.Remove("BearerToken");
                            context.Response.Headers.Add("BearerToken", newToken);
                        }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    _logger.LogError($"{nameAssembly} - {MethodBase.GetCurrentMethod()} - {ex.Message}");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"message\": \"Unauthorized: Invalid or expired token.\"}");
                    return;
                }
                catch(Exception ex)
                {
                    _logger.LogError($"{nameAssembly} - {MethodBase.GetCurrentMethod()} - {ex.Message}");
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"message\": \"Invalid token.\"}");
                    return;
                }
            }
            //else
            //{
            //    context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //    context.Response.ContentType = "application/json";
            //    await context.Response.WriteAsync("{\"message\": \"Forbidden: Need provide a token.\"}");
            //}
            await _next(context);
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.ContentType = "application/json";
                var response = new { message = "Unauthorized" };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
