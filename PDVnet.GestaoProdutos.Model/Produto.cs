using System;
using System.ComponentModel.DataAnnotations;

namespace PDVnet.GestaoProdutos.Model
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade do produto é obrigatória.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
        public decimal Quantidade { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Data da última atualização do registro (nullable para novos registros)
        public DateTime? DataAtualizacao { get; set; }
    }
}