using PDVnet.GestaoProdutos.Business;
using PDVnet.GestaoProdutos.Model;
using PDVnet.GestaoProdutos.UI.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.UI.ViewModels
{
    public class ProdutoFormViewModel : ObservableObject
    {
        private readonly ProdutoService _produtoService;
        private readonly IDialogService _dialogService;
        private Produto _produto = new Produto();
        private string _titulo = string.Empty;

        public ProdutoFormViewModel(Produto? produto = null, IDialogService? dialogService = null)
        {
            _produtoService = new ProdutoService();
            _dialogService = dialogService ?? new MessageBoxDialogService();

            if (produto == null)
            {
                // Modo ADICIONAR
                Produto = new Produto();
                Titulo = "Novo Produto";
            }
            else
            {
                // Modo EDITAR - cria uma cópia para não modificar o original diretamente
                Produto = new Produto
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Preco = produto.Preco,
                    Quantidade = produto.Quantidade,
                    DataCadastro = produto.DataCadastro
                };
                Titulo = "Editar Produto";
            }

            SalvarCommand = new RelayCommand(async () => await SalvarAsync(), PodeSalvar);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        public Produto Produto
        {
            get => _produto;
            set => SetProperty(ref _produto, value);
        }

        public string Titulo
        {
            get => _titulo;
            set => SetProperty(ref _titulo, value);
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        public bool Sucesso { get; private set; } = false;

        private async Task SalvarAsync()
        {
            var erros = ValidarProduto();
            if (erros.Count > 0)
            {
                var mensagemErro = "Erros de validação:\n" + string.Join("\n", erros);
                _dialogService.ShowError(mensagemErro, "Erro de Validação");
                return;
            }

            try
            {
                if (Produto.Id == 0) // Novo produto
                {
                    var (sucesso, errosValidacao) = await _produtoService.AdicionarAsync(Produto);
                    if (!sucesso)
                    {
                        MostrarErrosValidacao(errosValidacao);
                        return;
                    }
                }
                else // Editar produto existente
                {
                    var (sucesso, errosValidacao) = await _produtoService.AtualizarAsync(Produto);
                    if (!sucesso)
                    {
                        MostrarErrosValidacao(errosValidacao);
                        return;
                    }
                }

                Sucesso = true;
                FecharJanela();
            }
            catch (System.Exception ex)
            {
                _dialogService.ShowError($"Erro ao salvar produto: {ex.Message}", "Erro");
            }
        }

        private void Cancelar()
        {
            Sucesso = false;
            FecharJanela();
        }

        private bool PodeSalvar()
        {
            return Produto != null &&
                   !string.IsNullOrWhiteSpace(Produto.Nome) &&
                   Produto.Preco >= 0.01m &&
                   Produto.Quantidade >= 0;
        }

        private List<string> ValidarProduto()
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(Produto.Nome))
                erros.Add("• Nome é obrigatório");

            if (Produto.Preco < 0.01m)
                erros.Add("• Preço deve ser maior que zero");

            if (Produto.Quantidade < 0)
                erros.Add("• Quantidade não pode ser negativa");

            return erros;
        }

        private void MostrarErrosValidacao(List<ValidationResult> erros)
        {
            var mensagemErro = "Erros de validação:\n";
            foreach (var erro in erros)
            {
                mensagemErro += $"• {erro.ErrorMessage}\n";
            }
            _dialogService.ShowError(mensagemErro, "Erro de Validação");
        }

        private void FecharJanela()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}