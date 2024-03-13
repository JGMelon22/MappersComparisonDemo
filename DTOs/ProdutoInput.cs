using System.ComponentModel.DataAnnotations;

namespace MappersWebApiDemo.DTOs;

public record ProdutoInput
(
    [Required(ErrorMessage = "Nome do produto é obrigatório!")]
    [MinLength(2, ErrorMessage = "Nome do produto deve conter ao menos 2 caracteres!")]
    [MaxLength(50, ErrorMessage = "Nome do produto não pode exceder 50 caracteres!")]
    string Nome,

    [Required(ErrorMessage = "Preço cdo produto é obrigatório!")]
    [Range(0.25, 999.99, ErrorMessage = "O valor do produto deve ser entre R$0.25 a R$999.99!")]
    float Preco,

    [Required(ErrorMessage = "O status de disponibilidade do produto deve ser informado!")]
    bool Disponivel
);
