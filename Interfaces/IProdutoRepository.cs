namespace MappersWebApiDemo.Interfaces;

public interface IProdutoRepository
{
    Task<ServiceResponse<ProdutoResult>> AddProdutoAsync(ProdutoInput newProduto);
    Task<ServiceResponse<List<ProdutoResult>>> GetAllProdutosAsync();
    Task<ServiceResponse<ProdutoResult>> UpdateProdutoAsync(int id, ProdutoInput updatedProduct);
}