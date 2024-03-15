using MappersWebApiDemo.Infrastructure.Data;
using MappersWebApiDemo.Interfaces;

namespace MappersWebApiDemo.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _dbContext;

    public ProdutoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<ProdutoResult>> AddProdutoAsync(ProdutoInput newProduto)
    {
        var serviceResponse = new ServiceResponse<ProdutoResult>();

        try
        {
            var produto = new Produto // Necessidade de instanciar o objeto base e mapear com o input recebido
            {
                Nome = newProduto.Nome,
                Preco = newProduto.Preco,
                Disponivel = newProduto.Disponivel
            };

            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();

            // Mapeando o retorno manualmente
            var produtoResult = new ProdutoResult
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco
                // Não queremos mostrar para o usuário o campo "Disponível" (regra besta, releve)
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

            var produtosMapeados = new List<ProdutoResult>(); // Lista vazia para adicionar os itens mapeados

            foreach (var produto in produtos)
            {
                var produtoResult = new ProdutoResult // Mapeando os itens manualmente
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Preco = produto.Preco
                };

                produtosMapeados.Add(produtoResult);
            }

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

            produto.Nome = updatedProduct.Nome; // Mapeando o produtos encontrado com o Input do usuário
            produto.Preco = updatedProduct.Preco;
            produto.Disponivel = produto.Disponivel;

            await _dbContext.SaveChangesAsync();

            var produtoResult =
                new ProdutoResult // Mapeando o Produto atualizado para retornar apenas os campos desejados
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Preco = produto.Preco
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