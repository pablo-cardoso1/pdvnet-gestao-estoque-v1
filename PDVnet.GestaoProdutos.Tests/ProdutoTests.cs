using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDVnet.GestaoProdutos.Business.Validators;
using PDVnet.GestaoProdutos.Model;
using System.Collections.Generic;
using System.Linq;

namespace PDVnet.GestaoProdutos.Tests
{
    [TestClass]
    public class ProdutoTests
    {
        [TestMethod]
        public void Produto_ComDadosValidos_DevePassarValidacao()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste Válido",
                Descricao = "Descrição do produto teste",
                Preco = 99.99m,
                Quantidade = 10
            };

            // Act
            var erros = ProdutoValidator.Validar(produto);

            // Assert
            Assert.AreEqual(0, erros.Count, "Produto válido não deve ter erros de validação");
        }

        [TestMethod]
        public void Produto_SemNome_DeveFalharValidacao()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "", // Nome vazio - INVÁLIDO
                Preco = 50.00m,
                Quantidade = 5
            };

            // Act
            var erros = ProdutoValidator.Validar(produto);

            // Assert
            Assert.IsTrue(erros.Count > 0, "Produto sem nome deve ter erro de validação");
            Assert.IsTrue(erros.Any(e => e.ErrorMessage.Contains("obrigatório")),
                         "Deve conter mensagem de campo obrigatório");
        }

        [TestMethod]
        public void Produto_PrecoNegativo_DeveFalharValidacao()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Preco = -10.00m, // Preço negativo - INVÁLIDO
                Quantidade = 5
            };

            // Act
            var erros = ProdutoValidator.Validar(produto);

            // Assert
            Assert.IsTrue(erros.Count > 0, "Produto com preço negativo deve ter erro de validação");
        }

        [TestMethod]
        public void Produto_QuantidadeNegativa_DeveFalharValidacao()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Preco = 50.00m,
                Quantidade = -5 // Quantidade negativa - INVÁLIDO
            };

            // Act
            var erros = ProdutoValidator.Validar(produto);

            // Assert
            Assert.IsTrue(erros.Count > 0, "Produto com quantidade negativa deve ter erro de validação");
        }

        [TestMethod]
        public void Produto_PrecoZero_DeveFalharValidacao()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Preco = 0.00m, // Preço zero - INVÁLIDO (por causa do Range 0.01)
                Quantidade = 5
            };

            // Act
            var erros = ProdutoValidator.Validar(produto);

            // Assert
            Assert.IsTrue(erros.Count > 0, "Produto com preço zero deve ter erro de validação");
        }
    }
}