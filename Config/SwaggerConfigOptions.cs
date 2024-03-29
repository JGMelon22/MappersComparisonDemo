using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MappersWebApiDemo.Config;

public class SwaggerConfigOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

    public SwaggerConfigOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Title = "MappersWebApiDemo",
                Version = description.ApiVersion.ToString()
            });
    }
}