using System.Windows.Controls;
using System.Windows.Input;
using Cryptonly.ViewModels;

namespace Cryptonly.Views
{
    public partial class CryptoConverterPage : Page
    {
        public CryptoConverterPage()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Получаем ViewModel из DataContext
                var viewModel = DataContext as CryptoConverterViewModel;
                // Проверяем, что ViewModel и команда не null, затем выполняем команду
                if (viewModel?.ConvertCommand != null && viewModel.ConvertCommand.CanExecute(null))
                {
                    viewModel.ConvertCommand.Execute(null);
                }
            }
        }
    }
}