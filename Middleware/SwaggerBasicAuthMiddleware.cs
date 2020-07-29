using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Swagger.Ui.Extensions.Middleware
{
    public class SwaggerBasicAuthMiddleware : AbstractSwaggerAuthenticationMiddleware
    {
        public SwaggerBasicAuthMiddleware(RequestDelegate next, string username, string password, string swaggerUrlKey = "swagger", string headerName = "Authorization") : base(next, username, password, swaggerUrlKey, headerName)
        {

        }
        protected override string AuthenticationType => "Basic";

    }
}
