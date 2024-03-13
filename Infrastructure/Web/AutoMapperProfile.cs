using AutoMapper;

namespace MappersWebApiDemo.Infrastructure.Web;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Produto, ProdutoResult>();
        CreateMap<ProdutoInput, Produto>();
    }
}