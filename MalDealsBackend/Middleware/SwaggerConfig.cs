using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MalDeals.Controllers;
using MalDeals.Data;
using MalDeals.Services;
using Microsoft.OpenApi.Models;

namespace MalDeals.Middleware
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meine API", Version = "v1" });

            // JWT Bearer Token-Authentifizierung anpassen
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "bearer", // Schemaschlüssel "bearer"
                BearerFormat = "JWT", // Erläuterung des Formats
                In = ParameterLocation.Header,
                Description = "Paste your JWT token here, in the format 'Bearer <token>'"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
         }});
        });
        }
        public static void UseSwaggerUIConfiguration(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/swagger") || context.Request.Path.StartsWithSegments("/scalar"))
                {
                    using var scope = app.ApplicationServices.CreateScope();
                    var userService = scope.ServiceProvider.GetRequiredService<SwaggerUserService>();
                    string authHeader = context.Request.Headers["Authorization"];
                    if (string.IsNullOrEmpty(authHeader) || !await ValidateSwaggerUser(authHeader, userService))
                    {
                        // Fehlende oder falsche Authentifizierung → 401 Unauthorized senden
                        context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Scalar Docs\"";
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.CompleteAsync();
                        return;
                    }
                }
                await next();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meine API v1");

                // Optional: Login-Handling für Swagger
                c.InjectJavascript("/swagger-custom.js");
                c.InjectStylesheet("/swagger-custom.css");
            });
        }
        private static async Task<bool> ValidateSwaggerUser(string authHeader, SwaggerUserService userService)
        {
            if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                return false;

            string encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
            string decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));

            var credentials = decodedCredentials.Split(':', 2);
            if (credentials.Length != 2)
                return false;

            string username = credentials[0];
            string password = credentials[1];

            var user = await userService.GetSwaggerUserAsync(username, password);
            return user != null;
        }
    };
}