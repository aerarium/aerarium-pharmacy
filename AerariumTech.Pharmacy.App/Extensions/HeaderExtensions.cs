using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class HeaderExtensions
    {
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            return app.Use((context, next) =>
            {
                // use built-in xss protection
                context.Response.Headers.Add("X-XSS-Protection", "1;mode=block");

                // avoid clickjacking
                context.Response.Headers.Add("X-Frame-Options", "DENY");

                // block <style> and <script> if they're not specified as text/css or text/javascript
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

                return next();
            });
        }

        public static IApplicationBuilder UseXForwardedHeaders(this IApplicationBuilder app)
        {
            return app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}