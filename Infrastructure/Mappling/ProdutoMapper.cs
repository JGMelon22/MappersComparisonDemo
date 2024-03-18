using Riok.Mapperly.Abstractions;

namespace MappersWebApiDemo.Infrastructure.Mappling;

[Mapper]
public partial class ProdutoMapper
{
    public partial ProdutoResult ProdutoToProdutoResult(Produto produto);
    public partial Produto ProdutoToProdutoInput(ProdutoInput produto);
    public partial void ApplyUpdate(ProdutoInput produtoInput, Produto produto);
}