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
            var produto = newProduto.Adapt<Produto>();

            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();


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


            updatedProduct.Adapt(produto);

            await _dbContext.SaveChangesAsync();

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