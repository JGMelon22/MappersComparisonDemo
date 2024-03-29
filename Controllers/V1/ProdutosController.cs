using Asp.Versioning;
using MappersWebApiDemo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MappersWebApiDemo.Controllers.V1;

[ApiVersion("1.0")]
[ApiController]
[Route("manual-mapping/[controller]")]
[Route("v{version:apiVersion}/manual-mapping/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _repository;

    public ProdutosController([FromKeyedServices("manual")] IProdutoRepository repository)
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