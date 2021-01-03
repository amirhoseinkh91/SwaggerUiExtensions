using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Swagger.Ui.Extensions.Middleware
{
    public abstract class AbstractSwaggerAuthenticationMiddleware
    {
        protected readonly RequestDelegate next;
        protected readonly string swaggerUrlKey;
        protected readonly string headerName;
        protected readonly string username;
        protected readonly string password;
        protected abstract string AuthenticationType { get; }

        public AbstractSwaggerAuthenticationMiddleware(RequestDelegate next, string username, string password, string swaggerUrlKey = "swagger", string headerName = "Authorization")
        {
            this.next = next;
            this.swaggerUrlKey = swaggerUrlKey;
            this.headerName = headerName;
            this.username = username;
            this.password = password;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Make sure we are hitting the swagger path, and not doing it locally as it just gets annoying :-)
            if (context.Request.Path.Value.Contains($"/{swaggerUrlKey}", StringComparison.OrdinalIgnoreCase))
            {
                string authHeader = context.Request.Headers[headerName];
                if (authHeader != null && authHeader.StartsWith($"{AuthenticationType} "))
                {
                    // Get the encoded username and password
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();

                    // Decode from Base64 to string
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    // Split username and password
                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(':', 2)[1];

                    // Check if login is correct
                    if (IsAuthorized(username, password))
                    {
                        await next.Invoke(context);
                        return;
                    }
                }

                // Return authentication type (causes browser to show login dialog)
                context.Response.Headers["WWW-Authenticate"] = AuthenticationType;

                // Return unauthorized
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                await next.Invoke(context);
            }
        }

        public bool IsAuthorized(string username, string password)
        {
            return username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && password.Equals(password);
        }
    }
}
