using PDVnet.GestaoProdutos.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PDVnet.GestaoProdutos.Business.Validators
{
    public class ProdutoValidator
    {
        public static List<ValidationResult> Validar(Produto produto)
        {
            var resultados = new List<ValidationResult>();
            var contexto = new ValidationContext(produto);

            Validator.TryValidateObject(produto, contexto, resultados, true);
            return resultados;
        }
    }
}