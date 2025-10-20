using PDVnet.GestaoProdutos.Business;
using PDVnet.GestaoProdutos.Model;
using PDVnet.GestaoProdutos.UI.Services;
using PDVnet.GestaoProdutos.UI.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.UI.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly ProdutoService _produtoService;
        private readonly IDialogService _dialogService;
        private ObservableCollection<Produto> _produtos = new();
        private Produto? _produtoSelecionado;
        private int _totalProdutos;
        private decimal _valorTotalEstoque;
        private int _produtosEstoqueBaixo;
        private ObservableCollection<Produto> _produtosEstoqueBaixoLista = new();

        public MainViewModel(IDialogService? dialogService = null)
        {
            _produtoService = new ProdutoService();
            _dialogService = dialogService ?? new MessageBoxDialogService();

            Produtos = new ObservableCollection<Produto>();
            ProdutosEstoqueBaixoLista = new ObservableCollection<Produto>();

            CarregarProdutosCommand = new RelayCommand(async () => await CarregarProdutosAsync());
            NovoCommand = new RelayCommand(NovoProduto);
            EditarCommand = new RelayCommand(EditarProduto, PodeEditarOuExcluir);
            ExcluirCommand = new RelayCommand(ExcluirProduto, PodeEditarOuExcluir);

            // carregar inicialmente sem bloquear a UI
            _ = CarregarProdutosAsync();
        }

        public ObservableCollection<Produto> Produtos
        {
            get => _produtos;
            set => SetProperty(ref _produtos, value);
        }

        public Produto? ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set
            {
                SetProperty(ref _produtoSelecionado, value);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public int TotalProdutos
        {
            get => _totalProdutos;
            set => SetProperty(ref _totalProdutos, value);
        }

        public decimal ValorTotalEstoque
        {
            get => _valorTotalEstoque;
            set => SetProperty(ref _valorTotalEstoque, value);
        }

        public int ProdutosEstoqueBaixo
        {
            get => _produtosEstoqueBaixo;
            set => SetProperty(ref _produtosEstoqueBaixo, value);
        }

        public ObservableCollection<Produto> ProdutosEstoqueBaixoLista
        {
            get => _produtosEstoqueBaixoLista;
            set => SetProperty(ref _produtosEstoqueBaixoLista, value);
        }

        public RelayCommand CarregarProdutosCommand { get; }
        public RelayCommand NovoCommand { get; }
        public RelayCommand EditarCommand { get; }
        public RelayCommand ExcluirCommand { get; }

        private async Task CarregarProdutosAsync()
        {
            try
            {
                var produtos = await _produtoService.ObterTodosAsync();
                Produtos.Clear();
                foreach (var produto in produtos)
                {
                    Produtos.Add(produto);
                }
                CalcularEstatisticas();
            }
            catch (System.Exception ex)
            {
                _dialogService.ShowError($"Erro ao carregar produtos: {ex.Message}");
            }
        }

        private void NovoProduto()
        {
            var viewModel = new ProdutoFormViewModel(null, _dialogService);
            var form = new ProdutoForm(viewModel)
            {
                Owner = Application.Current.MainWindow
            };

            form.ShowDialog();

            if (viewModel.Sucesso)
            {
                _ = CarregarProdutosAsync();
                _dialogService.ShowInfo("Produto salvo com sucesso!", "Sucesso");
            }
        }

        private void EditarProduto()
        {
            if (ProdutoSelecionado != null)
            {
                var viewModel = new ProdutoFormViewModel(ProdutoSelecionado, _dialogService);
                var form = new ProdutoForm(viewModel)
                {
                    Owner = Application.Current.MainWindow
                };

                form.ShowDialog();

                if (viewModel.Sucesso)
                {
                    _ = CarregarProdutosAsync();
                    _dialogService.ShowInfo("Produto atualizado com sucesso!", "Sucesso");
                }
            }
        }

        private async void ExcluirProduto()
        {
            if (ProdutoSelecionado != null)
            {
                var confirm = _dialogService.ShowConfirm($"Deseja excluir o produto '{ProdutoSelecionado.Nome}'?", "Confirmar Exclusão");

                if (confirm)
                {
                    var (sucesso, mensagem) = await _produtoService.ExcluirAsync(ProdutoSelecionado.Id);

                    if (sucesso)
                    {
                        _dialogService.ShowInfo(mensagem, "Sucesso");
                        _ = CarregarProdutosAsync();
                    }
                    else
                    {
                        _dialogService.ShowError(mensagem, "Erro");
                    }
                }
            }
        }

        private bool PodeEditarOuExcluir()
        {
            return ProdutoSelecionado != null;
        }

        private void CalcularEstatisticas()
        {
            if (Produtos == null || Produtos.Count == 0)
            {
                TotalProdutos = 0;
                ValorTotalEstoque = 0;
                ProdutosEstoqueBaixo = 0;
                ProdutosEstoqueBaixoLista?.Clear();
                return;
            }

            TotalProdutos = Produtos.Count;
            ValorTotalEstoque = Produtos.Sum(p => p.Preco * p.Quantidade);

            var produtosBaixoEstoque = Produtos.Where(p => p.Quantidade < 5).ToList();
            ProdutosEstoqueBaixo = produtosBaixoEstoque.Count;
            ProdutosEstoqueBaixoLista = new ObservableCollection<Produto>(produtosBaixoEstoque);
        }
    }
}