using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Newtonsoft.Json;
using Cryptonly.Data;
using Cryptonly.Services;

namespace Cryptonly.ViewModels
{
    public class CryptoConverterViewModel : BaseViewModel
    {
        public ObservableCollection<CryptoShort> Cryptos { get; set; }
        public ICommand ConvertCommand { get; }

        private readonly CoinCapRepository _repository;
        private CryptoShort _selectedFromCrypto;
        private CryptoShort _selectedToCrypto;
        private decimal _amount;
        private string _result;

        public decimal Amount
        {
            get => _amount;
            set { SetProperty(ref _amount, value); }
        }
        public CryptoShort SelectedFromCrypto
        {
            get => _selectedFromCrypto;
            set { SetProperty(ref _selectedFromCrypto, value); }
        }
        public CryptoShort SelectedToCrypto
        {
            get => _selectedToCrypto;
            set { SetProperty(ref _selectedToCrypto, value); }
        }
        public string Result
        {
            get => _result;
            set { SetProperty(ref _result, value); }
        }


        public CryptoConverterViewModel()
        {
            _repository = new CoinCapRepository();
            Cryptos = new ObservableCollection<CryptoShort>();
            ConvertCommand = new RelayCommand(async () => await ConvertCrypto());
            LoadCryptos();
        }

        // Loads the list of cryptocurrencies from the API and populates the Cryptos collection.
        private async void LoadCryptos()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync("https://api.coincap.io/v2/assets");
                var cryptoData = JsonConvert.DeserializeObject<CryptoShortList>(response);

                Cryptos.Clear();
                foreach (var crypto in cryptoData.Data)
                {
                    Cryptos.Add(crypto);
                }
            }
        }

        // Converts the amount from one cryptocurrency to another and updates the Result property.
        private async Task ConvertCrypto()
        {
            if (SelectedFromCrypto == null)
            {
                Result = LocalizationHelper.GetValue("NoFromCryptoSelected");
                return;
            }

            if (SelectedToCrypto == null)
            {
                Result = LocalizationHelper.GetValue("NoToCryptoSelected");
                return;
            }

            if (Amount > 0)
            {
                var fromPrice = await GetCryptoPriceInUsd(SelectedFromCrypto.Id);
                var toPrice = await GetCryptoPriceInUsd(SelectedToCrypto.Id);

                var convertedAmount = Amount * (fromPrice / toPrice);
                Result = $"{Amount} {SelectedFromCrypto.DisplayName} = {convertedAmount:F6} {SelectedToCrypto.DisplayName}";
            }
            else
            {
                Result = LocalizationHelper.GetValue("EnterAmount");
            }
        }

        // Retrieves the price of a cryptocurrency in USD from the repository.
        private async Task<decimal> GetCryptoPriceInUsd(string cryptoId)
        {
            var response = await _repository.FindCryptoCurrencyAsync(cryptoId);

            return response != null ? response.Data.PriceUsd : 0.0m;
        }
    }
}