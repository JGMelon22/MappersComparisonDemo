using FakeItEasy;
using FluentAssertions;
using MappersWebApiDemo.Controllers;
using MappersWebApiDemo.DTOs;
using MappersWebApiDemo.Interfaces;
using MappersWebApiDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace MappersWebApiDemo.Tests.Controller;

public class ProdutosAutoMapperControllerTests
{
    private readonly ProdutosAutoMapperController _produtosController;
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosAutoMapperControllerTests()
    {
        _produtoRepository = A.Fake<IProdutoRepository>();
        _produtosController = new ProdutosAutoMapperController(_produtoRepository);
    }

    [Fact]
    [Trait("ProdutosAutoMapperController", "GetAllProdutosAsync")]
    public void ProdutosAutoMapperController_GetAllProdutosAsync_ReturnsProdutos()
    {
        // Arrange
        var produtos = A.Fake<ServiceResponse<List<ProdutoResult>>>();
        A.CallTo(() => _produtoRepository.GetAllProdutosAsync()).Returns(produtos);

        // Act
        var result = _produtosController.GetAllProdutosAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("ProdutosAutoMapperController", "AddProdutoAsync")]
    public void ProdutosAutoMapperController_AddProdutoAsync_ReturnsProduto()
    {
        // Arrange
        var newProduto = A.Fake<ProdutoInput>();
        var produtoResult = A.Fake<ServiceResponse<ProdutoResult>>();
        A.CallTo(() => _produtoRepository.AddProdutoAsync(newProduto)).Returns(produtoResult);

        // Act
        var result = _produtosController.AddProdutoAsync(newProduto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("ProdutosAutoMapperController", "UpdateProdutoAsync")]
    public void ProdutosAutoMapperController_UpdateProdutoAsync_ReturnsProduto()
    {
        // Arrange
        int id = 13;
        var updatedProduto = A.Fake<ProdutoInput>();
        var produtoResult = A.Fake<ServiceResponse<ProdutoResult>>();
        A.CallTo(() => _produtoRepository.UpdateProdutoAsync(id, updatedProduto)).Returns(produtoResult);

        // Act
        var result = _produtosController.UpdateProdutoAsync(id, updatedProduto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }
}