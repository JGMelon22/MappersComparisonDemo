namespace MappersWebApiDemo.Models;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public float Preco { get; set; }
    public bool Disponivel { get; set; }
}