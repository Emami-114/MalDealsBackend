using MalDealsBackend.Services;

namespace MalDealsBackend.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiKeyService _apiKeyService;

        public ApiKeyMiddleware(RequestDelegate next, ApiKeyService apiKeyService)
        {
            _next = next;
            _apiKeyService = apiKeyService;
        }

        public async Task Invoke(HttpContext context)
        {

            var path = (context.Request.Path.Value ?? "").ToLower();
            var apiKey = context.Request.Query["api_key"];

            if (path.StartsWith("/scalar") || path.StartsWith("/swagger") || path.StartsWith("/openapi"))
            {
                await _next(context);
                return;
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is missing");
                return;
            }


            if (!_apiKeyService.ValidateApiKey(apiKey, out var deviceId))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }
            await _next(context);
        }
    }
}