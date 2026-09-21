using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace App.Infrastructure.Security.Services
{
    public class ApiKeySecurityTransformer : IOpenApiDocumentTransformer
    {
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            const string schemeName = "ApiKey";

            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

            // Register the authentication input format rules
            document.Components.SecuritySchemes[schemeName] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                Name = "X-API-KEY",
                In = ParameterLocation.Header,
                Description = "Enter your secret API key to access secure endpoints"
            };

            document.Security ??= [];

            var requirement = new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(schemeName, document)] = new List<string>()
            };

            document.Security.Add(requirement);
            return Task.CompletedTask;
        }
    }
}
