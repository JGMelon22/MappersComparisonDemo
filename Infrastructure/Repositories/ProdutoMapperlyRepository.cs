using MappersWebApiDemo.Infrastructure.Data;
using MappersWebApiDemo.Infrastructure.Mappling;
using MappersWebApiDemo.Interfaces;

namespace MappersWebApiDemo.Infrastructure.Repositories;

public class ProdutoMapperlyRepository : IProdutoRepository
{
    private readonly AppDbContext _dbContext;
    public ProdutoMapperlyRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<ProdutoResult>> AddProdutoAsync(ProdutoInput newProduto)
    {
        var serviceResponse = new ServiceResponse<ProdutoResult>();

        try
        {
            var mapper = new ProdutoMapper();
            var produto = mapper.ProdutoToProdutoInput(newProduto);

            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();

            var produtoResult = mapper.ProdutoToProdutoResult(produto);

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

            var mapper = new ProdutoMapper();
            var produtosMapeados = produtos.Select(x => mapper.ProdutoToProdutoResult(x)).ToList();

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

            var mapper = new ProdutoMapper();
            mapper.ProdutoToProdutoInput(updatedProduct);

            await _dbContext.SaveChangesAsync();

            var produtoResult = mapper.ProdutoToProdutoResult(produto);

            serviceResponse.Data = produtoResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}