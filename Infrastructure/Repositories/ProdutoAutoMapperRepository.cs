using AutoMapper;
using MappersWebApiDemo.Infrastructure.Data;
using MappersWebApiDemo.Interfaces;

namespace MappersWebApiDemo.Infrastructure.Repositories;

public class ProdutoAutoMapperRepository : IProdutoRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProdutoAutoMapperRepository(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<ProdutoResult>> AddProdutoAsync(ProdutoInput newProduto)
    {
        var serviceResponse = new ServiceResponse<ProdutoResult>();

        try
        {
            var produto = _mapper.Map<Produto>(newProduto);

            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();

            var produtoResult = _mapper.Map<ProdutoResult>(produto);


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

            var produtosMapeados = produtos.Select(x => _mapper.Map<ProdutoResult>(x)).ToList();

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

            produto.Nome = updatedProduct.Nome;
            produto.Preco = updatedProduct.Preco;
            produto.Disponivel = produto.Disponivel;

            await _dbContext.SaveChangesAsync();

            var produtoResult = new ProdutoResult
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
            };

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