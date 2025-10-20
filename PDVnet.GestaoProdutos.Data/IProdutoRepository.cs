using PDVnet.GestaoProdutos.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Data
{
    public interface IProdutoRepository
    {
        List<Produto> ObterTodos();
        Task<List<Produto>> ObterTodosAsync();

        Produto? ObterPorId(int id);
        Task<Produto?> ObterPorIdAsync(int id);

        void Adicionar(Produto produto);
        Task AdicionarAsync(Produto produto);

        void Atualizar(Produto produto);
        Task AtualizarAsync(Produto produto);

        bool Excluir(int id);
        Task<bool> ExcluirAsync(int id);
    }
}
