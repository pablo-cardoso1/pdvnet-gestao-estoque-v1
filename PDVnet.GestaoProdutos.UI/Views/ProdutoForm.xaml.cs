using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
using System.Windows.Controls;
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

        // Quando o TextBox recebe foco, seleciona todo o texto para que a primeira digitação substitua o conteúdo
        private void TextBox_SelectAll(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.SelectAll();
            }
        }

        // Garante que um clique com o mouse no TextBox também selecione tudo (em vez de só posicionar o caret)
        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && !tb.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                tb.Focus();
            }
        }

        // Permite digitar números e separador decimal (ponto ou vírgula)
        private void NumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var tb = sender as TextBox;

            // If the textbox currently shows the formatted zero (e.g. "0,00" or "0.00"),
            // replace it on the first key press so the user doesn't end up with "32.25.00".
            if (tb != null)
            {
                var formattedZero = 0m.ToString("N2", CultureInfo.CurrentCulture);

                if (tb.Text == formattedZero && _decimalRegex.IsMatch(e.Text))
                {
                    // Replace the entire text with the newly typed character(s)
                    tb.Text = e.Text;
                    tb.CaretIndex = tb.Text.Length;
                    e.Handled = true; // we've handled the input
                    return;
                }
            }

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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}