using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PDVnet.GestaoProdutos.Business;
using PDVnet.GestaoProdutos.Data;
using PDVnet.GestaoProdutos.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Tests
{
    [TestClass]
    public class ProdutoServiceTests
    {
        [TestMethod]
        public async Task AdicionarAsync_ProdutoValido_ChamaRepositorio()
        {
            // Arrange
            var mockRepo = new Mock<IProdutoRepository>();
            mockRepo.Setup(r => r.AdicionarAsync(It.IsAny<Produto>())).Returns(Task.CompletedTask).Verifiable();

            var service = new ProdutoService(mockRepo.Object);

            var produto = new Produto
            {
                Nome = "Teste",
                Preco = 10.0m,
                Quantidade = 2
            };

            // Act
            var (sucesso, erros) = await service.AdicionarAsync(produto);

            // Assert
            Assert.IsTrue(sucesso);
            Assert.AreEqual(0, erros.Count);
            mockRepo.Verify(r => r.AdicionarAsync(It.IsAny<Produto>()), Times.Once);
        }

        [TestMethod]
        public async Task AdicionarAsync_ProdutoInvalido_NaoChamaRepositorio()
        {
            // Arrange
            var mockRepo = new Mock<IProdutoRepository>();
            var service = new ProdutoService(mockRepo.Object);

            var produto = new Produto
            {
                Nome = "", // inválido
                Preco = 0m,
                Quantidade = -1
            };

            // Act
            var (sucesso, erros) = await service.AdicionarAsync(produto);

            // Assert
            Assert.IsFalse(sucesso);
            Assert.IsTrue(erros.Count > 0);
            mockRepo.Verify(r => r.AdicionarAsync(It.IsAny<Produto>()), Times.Never);
        }
    }
}
