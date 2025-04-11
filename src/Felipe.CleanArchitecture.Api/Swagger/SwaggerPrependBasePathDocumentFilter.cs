using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Felipe.CleanArchitecture.Api.Swagger;

public class SwaggerPrependBasePathDocumentFilter(string basePath) : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Servers =
            [
                new OpenApiServer { Url = basePath }
            ];
    }
}
