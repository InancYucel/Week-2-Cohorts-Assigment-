using System.Net;

namespace WonderFulGraphicCards_API.Middlewares;

public class CustomMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}