using Asp.Versioning;
using MappersWebApiDemo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MappersWebApiDemo.Controllers.V2;

[ApiVersion("2.0")]
[ApiController]
[Route("v{version:apiVersion}/automapper-mapping/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _repository;

    public ProdutosController([FromKeyedServices("automapper")] IProdutoRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> AddProdutoAsync(ProdutoInput newProduto)
    {
        var produto = await _repository.AddProdutoAsync(newProduto);
        return produto.Data != null
            ? Ok(produto)
            : BadRequest(produto);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateProdutoAsync(int id, ProdutoInput updatedProduct)
    {
        var produto = await _repository.UpdateProdutoAsync(id, updatedProduct);
        return produto.Data != null
            ? Ok(produto)
            : BadRequest(produto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProdutosAsync()
    {
        var produtos = await _repository.GetAllProdutosAsync();
        return produtos.Data != null
            ? Ok(produtos)
            : NoContent();
    }
}