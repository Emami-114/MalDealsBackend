using MalDealsBackend.Services;

namespace MalDealsBackend.Middleware
{
    public class ApiKeyMiddleware(RequestDelegate next, ApiKeyService apiKeyService)
    {
        private readonly RequestDelegate _next = next;
        private readonly ApiKeyService _apiKeyService = apiKeyService;

        public async Task Invoke(HttpContext context)
        {

            var path = (context.Request.Path.Value ?? "").ToLower();
            var apiKey = context.Request.Headers["X-API-Key"]; // API-Key auch aus dem Header holen

            if (path.StartsWith("/scalar") || path.StartsWith("/swagger") || path.StartsWith("/openapi") || path.StartsWith("/api/request-api-key"))
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

            if (apiKey == Environment.GetEnvironmentVariable("API_KEY_SECRET_KEY")) {
                await _next(context);
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