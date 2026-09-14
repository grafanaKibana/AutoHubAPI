using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AutoHub.API.Middlewares;

public class RedirectMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Request.Path == "/")
        {
            httpContext.Response.Redirect("/index.html");
            return;
        }

        await next(httpContext);
    }
}
