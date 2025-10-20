using System.Windows;
using PDVnet.GestaoProdutos.UI.ViewModels;

namespace PDVnet.GestaoProdutos.UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Preferível: receber via DI. Aqui criamos localmente para compatibilidade.
            DataContext = new MainViewModel();
        }
    }
}