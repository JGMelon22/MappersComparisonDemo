using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MappersWebApiDemo.Config;

public class SwaggerDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var description in context.ApiDescriptions)
            if (description.ParameterDescriptions.Any(p => p.Name == "api-version" && p.Source.Id == "Query"))
                swaggerDoc.Paths.Remove($"/{description.RelativePath?.TrimEnd('/')}");
    }
}