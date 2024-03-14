using AutoMapper;

namespace MappersWebApiDemo.Infrastructure.Web;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Produto, ProdutoResult>(); // Configuração para mapear a pesquisa
        CreateMap<ProdutoInput, Produto>(); // Configuração para mapear a inserção
    }
}