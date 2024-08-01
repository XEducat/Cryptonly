using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Input;
using Cryptonly.Data;
using static Cryptonly.MainWindow;

namespace Cryptonly
{
    public partial class PopularCryptosPage : Page
    {
        public PopularCryptosPage()
        {
            InitializeComponent();
            LoadCryptoData();
        }

        private async void LoadCryptoData()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync("https://api.coincap.io/v2/assets?limit=10");
                var cryptoData = JsonConvert.DeserializeObject<List<CryptoCurrencyShort>>(response);

                cryptoListView.ItemsSource = cryptoData;
            }
        }

        private void CryptoListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (cryptoListView.SelectedItem is CryptoCurrencyShort selectedCrypto)
            {
                var detailPage = new CryptoDetailPage(selectedCrypto);
                NavigationService.Navigate(detailPage);
            }
        }
    }
}
