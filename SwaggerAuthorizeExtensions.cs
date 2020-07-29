using Microsoft.AspNetCore.Builder;
using Swagger.Ui.Extensions.Middleware;
using System;
using System.Linq;
using System.Reflection;

namespace Swagger.Ui.Extensions
{
    public static class SwaggerAuthorizeExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthenticated<T>(this IApplicationBuilder builder, string username, string password, string swaggerUrlKey = "swagger", string headerName = "Authorization") where T : AbstractSwaggerAuthenticationMiddleware
        {
            return builder.UseMiddleware<T>(username, password, swaggerUrlKey, headerName);
        }

        public static IApplicationBuilder UseSwaggerAuthenticated(this IApplicationBuilder builder, string username, string password, string swaggerUrlKey = "swagger", string headerName = "Authorization")
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>(username, password, swaggerUrlKey, headerName);
        }
    }
}
