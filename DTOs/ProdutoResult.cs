namespace MappersWebApiDemo.DTOs;

public record ProdutoResult
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public float Preco { get; init; }
}
