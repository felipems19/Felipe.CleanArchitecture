using Microsoft.AspNetCore.Http;

namespace Felipe.CleanArchitecture.Application.Common.Middlewares;

public class RemoveServerHeaderMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        _ = context.Request.Headers.ToList();
        context.Response.Headers.Remove("Server");
        context.Response.Headers.Remove("X-Powered-By");
        context.Response.Headers.Remove("X-Server-Powered-By");
        context.Response.Headers.Remove("X-AspNet-Version");

        await next(context);
    }
}
