using Cryptonly.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows.Controls;

namespace Cryptonly
{
    public partial class CryptoDetailPage : Page
    {
        public CryptoDetailPage(CryptoCurrencyShort selectedCrypto)
        {
            InitializeComponent();
            LoadCryptoDetailsAsync(selectedCrypto);
            DataContext = selectedCrypto; // Устанавливаем DataContext для привязки данных
        }
        private async Task LoadCryptoDetailsAsync(CryptoCurrencyShort selectedCrypto)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Replace with the actual API endpoint for fetching detailed data
                    var response = await client.GetStringAsync($"api.coincap.io/v2/assets/{selectedCrypto.Id}");
                    var detailedData = JsonConvert.DeserializeObject<CryptoCurrencyShort>(response);

                    // Update the DataContext with the detailed data
                    DataContext = detailedData;
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    // For example, you might want to show an error message or log the error
                }
            }
        }
    }
}
