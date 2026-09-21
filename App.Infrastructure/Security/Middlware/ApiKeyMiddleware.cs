using App.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;


namespace App.Infrastructure.Security.Middlware
{
    public class ApiKeyMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private const string ApiKeyHeaderName = "X-API-KEY";

        public async Task InvokeAsync(HttpContext context, IApiKeyValidator apiKeyValidator)
        {

            var path = context.Request.Path.Value ?? string.Empty;

            // 1. CRITICAL: Bypass API Key checks for Browser UI Assets & Specification files
            if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith("/scalar", StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith("/openapi", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key was not provided");
                return;
            }

            if (string.IsNullOrEmpty(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }

            if(!await apiKeyValidator.IsValidAsync(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }


            await _next(context);

        }
    }
}
