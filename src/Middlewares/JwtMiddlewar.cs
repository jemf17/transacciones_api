using System.IdentityModel.Tokens.Jwt;
using System.Text;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//using System.Threading.Tasks;
//using System.Linq;
using DotNetEnv;

namespace middlewares{
public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _secretKey;

    public JwtMiddleware(RequestDelegate next)
{   
    Env.Load();
    _next = next;
    _secretKey = Environment.GetEnvironmentVariable("SECRET_KEY") 
                 ?? throw new ArgumentNullException(nameof(_secretKey), "SecretKey no puede ser nulo");
}

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Query["token"].FirstOrDefault(); // Obtener el JWT desde los parámetros

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_secretKey);
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out _);
                context.Items["JwtClaims"] = principal.Claims.ToDictionary(c => c.Type, c => c.Value);
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token inválido");
                return;
            }
        }

        await _next(context);
    }
}
}
