using MappersWebApiDemo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MappersWebApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosMapsterController : ControllerBase
{
    private readonly IProdutoRepository _repository;

    public ProdutosMapsterController(IProdutoRepository repository)
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