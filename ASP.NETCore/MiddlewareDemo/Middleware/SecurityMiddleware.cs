using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace MiddlewareDemo
{
    public class SecurityMiddleware
    {
        private RequestDelegate _next;

        public SecurityMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("<br/> Security Middleware -  Request processed");
            await _next.Invoke(context);
            await context.Response.WriteAsync("<br/> Security Middleware -  Response processed");
        }
    }

    public static class MiddlewareExtensions
    {
        public static void UseSecurity(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecurityMiddleware>();
        }
    }
    
}