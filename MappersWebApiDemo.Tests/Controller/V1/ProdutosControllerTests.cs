using FakeItEasy;
using FluentAssertions;
using MappersWebApiDemo.Controllers.V1;
using MappersWebApiDemo.DTOs;
using MappersWebApiDemo.Interfaces;
using MappersWebApiDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace MappersWebApiDemo.Controller.V1;

public class ProdutosControllerTests
{
    private readonly ProdutosController _controller;
    private readonly IProdutoRepository _repository;

    public ProdutosControllerTests()
    {
        _repository = A.Fake<IProdutoRepository>();

        // SUT
        _controller = new ProdutosController(_repository);
    }

    [Fact]
    [Trait("ProdutosController", "GetAllProdutosAsync")]
    public void ProdutosController_GetAllProdutosAsync_ReturnsProtudos()
    {
        // Arrange
        var produtos = A.Fake<ServiceResponse<List<ProdutoResult>>>();
        A.CallTo(() => _repository.GetAllProdutosAsync()).Returns(produtos);

        // Act
        var result = _controller.GetAllProdutosAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("ProdutosController", "AddProdutoAsync")]
    public void ProdutosController_AddProdutoAsync_ReturnsProtudo()
    {
        // Arrange
        var newProduto = A.Fake<ProdutoInput>();
        var produtoResult = A.Fake<ServiceResponse<ProdutoResult>>();
        A.CallTo(() => _repository.AddProdutoAsync(newProduto)).Returns(produtoResult);

        // Act
        var result = _controller.AddProdutoAsync(newProduto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("ProdutosController", "UpdateProdutoAsync")]
    public void ProdutosController_UpdateProdutoAsync_ReturnsProtudo()
    {
        // Arrange
        int id = 1;
        var updatedProduto = A.Fake<ProdutoInput>();
        var produtoResult = A.Fake<ServiceResponse<ProdutoResult>>();
        A.CallTo(() => _repository.UpdateProdutoAsync(id, updatedProduto)).Returns(produtoResult);

        // Act
        var result = _controller.UpdateProdutoAsync(id, updatedProduto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }
}