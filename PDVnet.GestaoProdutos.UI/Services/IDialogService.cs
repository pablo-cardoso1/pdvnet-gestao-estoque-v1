namespace PDVnet.GestaoProdutos.UI.Services
{
    public interface IDialogService
    {
        void ShowInfo(string message, string title = "Informa��o");
        void ShowError(string message, string title = "Erro");
        bool ShowConfirm(string message, string title = "Confirmar");
    }
}