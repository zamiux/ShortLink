using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using ShortLink.Application.Interfaces;
using System.Threading.Tasks;

namespace ShortLink.Web.Middelware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ShortLinkUrl
    {
        private readonly RequestDelegate _next;
        private ILinkService _linkService;

        public ShortLinkUrl(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _linkService = (ILinkService)httpContext.RequestServices.GetService(typeof(ILinkService));
            var userAgent = StringValues.Empty;
            // get user-agent
            httpContext.Request.Headers.TryGetValue("User-Agent", out userAgent);

            if (httpContext.Request.Path.ToString().Length == 9)
            {
                // add user-agent in db
                await _linkService.AddUserAgent(userAgent);

                // get token    
                var token = httpContext.Request.Path.ToString().Substring(1);
                var shortUrl = await _linkService.FindUrlByToken(token);

                //submit log user request for url
                await _linkService.AddRequestUrl(token);

                if (shortUrl != null)
                {
                    httpContext.Response.Redirect(shortUrl.OrginalUrl.ToString());
                }
                else
                {
                    httpContext.Response.Redirect(httpContext.Request.Host.ToString());
                }
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ShortLinkUrlExtensions
    {
        public static IApplicationBuilder UseShortLinkUrl(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ShortLinkUrl>();
        }
    }
}
