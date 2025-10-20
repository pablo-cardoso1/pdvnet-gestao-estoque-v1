using PDVnet.GestaoProdutos.Business.Validators;
using PDVnet.GestaoProdutos.Data;
using PDVnet.GestaoProdutos.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Business
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository? repository = null)
        {
            _repository = repository ?? new ProdutoRepository();
        }

        public List<Produto> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public async Task<List<Produto>> ObterTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public Produto? ObterPorId(int id)
        {
            return _repository.ObterPorId(id);
        }

        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public (bool sucesso, List<ValidationResult> erros) Adicionar(Produto produto)
        {
            var erros = ProdutoValidator.Validar(produto);

            if (erros.Count == 0)
            {
                _repository.Adicionar(produto);
                return (true, erros);
            }

            return (false, erros);
        }

        public async Task<(bool sucesso, List<ValidationResult> erros)> AdicionarAsync(Produto produto)
        {
            var erros = ProdutoValidator.Validar(produto);

            if (erros.Count == 0)
            {
                await _repository.AdicionarAsync(produto);
                return (true, erros);
            }

            return (false, erros);
        }

        public (bool sucesso, List<ValidationResult> erros) Atualizar(Produto produto)
        {
            var erros = ProdutoValidator.Validar(produto);
            if (erros.Count == 0)
            {
                _repository.Atualizar(produto);
                return (true, erros);
            }
            return (false, erros);
        }

        public async Task<(bool sucesso, List<ValidationResult> erros)> AtualizarAsync(Produto produto)
        {
            var erros = ProdutoValidator.Validar(produto);
            if (erros.Count == 0)
            {
                await _repository.AtualizarAsync(produto);
                return (true, erros);
            }
            return (false, erros);
        }

        public (bool sucesso, string mensagem) Excluir(int id)
        {
            var produto = _repository.ObterPorId(id);
            if (produto != null)
            {
                _repository.Excluir(id);
                return (true, "Produto excluído com sucesso.");
            }
            return (false, "Produto não encontrado.");
        }

        public async Task<(bool sucesso, string mensagem)> ExcluirAsync(int id)
        {
            var produto = await _repository.ObterPorIdAsync(id);
            if (produto != null)
            {
                await _repository.ExcluirAsync(id);
                return (true, "Produto excluído com sucesso.");
            }
            return (false, "Produto não encontrado.");
        }
    }
}