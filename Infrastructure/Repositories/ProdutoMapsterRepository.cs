using MappersWebApiDemo.Infrastructure.Data;
using MappersWebApiDemo.Interfaces;
using Mapster;

namespace MappersWebApiDemo.Infrastructure.Repositories;

public class ProdutoMapsterRepository : IProdutoRepository
{
    private readonly AppDbContext _dbContext;
    public ProdutoMapsterRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<ProdutoResult>> AddProdutoAsync(ProdutoInput newProduto)
    {
        var serviceResponse = new ServiceResponse<ProdutoResult>();

        try
        {
            // Utilizando o méotodo .Adapt, mapeamos o conteúdo vindo de ProdutoInput para a nossa model
            var produto = newProduto.Adapt<Produto>();

            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();

            // E novamente mapeamos para o resultado com os campos que queremos listas da DTO de resultado
            var produtoResult = produto.Adapt<ProdutoResult>();

            serviceResponse.Data = produtoResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<ProdutoResult>>> GetAllProdutosAsync()
    {
        var serviceResponse = new ServiceResponse<List<ProdutoResult>>();

        try
        {
            var produtos = await _dbContext
                .Produtos
                .AsNoTracking()
                .ToListAsync()
                ?? throw new Exception("Lista de Produtos vazia!");

            // Similar ao AtuoMapper, com apenas uma linha de código mapeamos a nossa lista, porém com o método .Adapt
            var produtosMapeados = produtos.Adapt<List<ProdutoResult>>().ToList();

            serviceResponse.Data = produtosMapeados;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<ProdutoResult>> UpdateProdutoAsync(int id, ProdutoInput updatedProduct)
    {
        var serviceResponse = new ServiceResponse<ProdutoResult>();

        try
        {
            var produto = await _dbContext
                .Produtos
                .FindAsync(id)
                ?? throw new Exception("Produto não encontrado!");

            // Mapeando o conteúdo do input para refletir no produto pesquisado a ser atualizado
            updatedProduct.Adapt(produto);

            await _dbContext.SaveChangesAsync();
            
            // E retornamos ele mapeado para a DTO de resultado
            serviceResponse.Data = produto.Adapt<ProdutoResult>();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}