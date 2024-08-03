using System.Windows.Controls;
using System.Windows.Input;
using Cryptonly.Data;
using Cryptonly.ViewModels;

namespace Cryptonly
{
    public partial class PopularCryptosPage : Page
    {
        public PopularCryptosPage()
        {
            InitializeComponent();
            DataContext = new PopularCryptosViewModel();
        }

        private void CryptoListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ListView)sender).SelectedItem is CryptoCurrencyShort selectedCrypto)
            {
                var detailPage = new CryptoDetailPage(selectedCrypto);
                NavigationService.Navigate(detailPage);
            }
        }
    }
}
