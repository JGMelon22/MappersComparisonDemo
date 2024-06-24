using MappersWebApiDemo.Infrastructure.Repositories;
using MappersWebApiDemo.Interfaces;

namespace MappersComparisonDemo.Configuration;

public static class RepositoriesCollection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddKeyedScoped<IProdutoRepository, ProdutoRepository>("manual");
        services.AddKeyedScoped<IProdutoRepository, ProdutoAutoMapperRepository>("automapper");
        services.AddKeyedScoped<IProdutoRepository, ProdutoMapsterRepository>("mapster");
        services.AddKeyedScoped<IProdutoRepository, ProdutoMapperlyRepository>("mapperly");

        return services;
    }
}