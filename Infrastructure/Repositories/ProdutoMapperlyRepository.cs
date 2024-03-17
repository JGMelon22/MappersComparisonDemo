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
            // Instanciamos nossa classe "especial" e
            // a partir do "ProdutoToProdutoInput" mapeamos o 
            // Novo Produto vindo do input para nossa model
            var mapper = new ProdutoMapper();
            var produto = mapper.ProdutoToProdutoInput(newProduto);

            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();


            // Em seguida, com intuito de mostrar para o usuário o novo produto adicionado como feedback
            // Mapeamos para o result DTO
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

            // Por não estarmos trabalhando com injeção de dependência, precisamos declarar a classe "especial" e
            // em seguida chamarmos o método responsável por mapear a lista de produtos para respeitar as propriedades da DTO de resultado
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

            // Mapear o input de dados para refletir na model
            produto.Nome = updatedProduct.Nome;
            produto.Preco = updatedProduct.Preco;
            produto.Disponivel = updatedProduct.Disponivel;

            await _dbContext.SaveChangesAsync();

            // Mapear o produto atualizado para retornar suas informações baseadas no que consta na result DTO
            var mapper = new ProdutoMapper();
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