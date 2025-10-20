namespace PDVnet.GestaoProdutos.UI.Services
{
    public interface IDialogService
    {
        void ShowInfo(string message, string title = "Informação");
        void ShowError(string message, string title = "Erro");
        bool ShowConfirm(string message, string title = "Confirmar");
    }
}