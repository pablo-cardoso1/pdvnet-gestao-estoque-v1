using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using PDVnet.GestaoProdutos.UI.ViewModels;

namespace PDVnet.GestaoProdutos.UI.Views
{
    public partial class ProdutoForm : Window
    {
        private static readonly Regex _decimalRegex = new(@"^[0-9]*(?:[.,][0-9]*)?$");
        private static readonly Regex _intRegex = new(@"^\d+$");

        public ProdutoForm()
        {
            InitializeComponent();
        }

        public ProdutoForm(ProdutoFormViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }

        // Permite digitar números e separador decimal (ponto ou vírgula)
        private void NumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_decimalRegex.IsMatch(e.Text);
        }

        // Permite apenas dígitos inteiros
        private void IntegerOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_intRegex.IsMatch(e.Text);
        }

        // Trata colagem para campo decimal
        private void NumericOnly_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                var text = e.DataObject.GetData(DataFormats.Text) as string ?? string.Empty;
                if (!_decimalRegex.IsMatch(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        // Trata colagem para campo inteiro
        private void IntegerOnly_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                var text = e.DataObject.GetData(DataFormats.Text) as string ?? string.Empty;
                if (!_intRegex.IsMatch(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}