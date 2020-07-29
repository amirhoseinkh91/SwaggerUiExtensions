using Microsoft.AspNetCore.Builder;
using Swagger.Ui.Extensions.Middleware;
using System;
using System.Linq;
using System.Reflection;

namespace Swagger.Ui.Extensions
{
    public static class SwaggerAuthorizeExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthenticated<T>(this IApplicationBuilder builder) where T : AbstractSwaggerAuthenticationMiddleware
        {
            return builder.UseMiddleware<T>();
        }

        public static IApplicationBuilder UseSwaggerAuthenticated(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
    }
}
