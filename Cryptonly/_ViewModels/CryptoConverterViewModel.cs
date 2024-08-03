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
        public ObservableCollection<CryptoCurrencyShort> Cryptos { get; set; }
        public ICommand ConvertCommand { get; }

        private readonly CoinCapRepository _repository;
        private CryptoCurrencyShort _selectedFromCrypto;
        private CryptoCurrencyShort _selectedToCrypto;
        private decimal _amount;
        private string _result;

        public decimal Amount
        {
            get => _amount;
            set { SetProperty(ref _amount, value); }
        }
        public CryptoCurrencyShort SelectedFromCrypto
        {
            get => _selectedFromCrypto;
            set { SetProperty(ref _selectedFromCrypto, value); }
        }
        public CryptoCurrencyShort SelectedToCrypto
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
            Cryptos = new ObservableCollection<CryptoCurrencyShort>();
            ConvertCommand = new RelayCommand(async () => await ConvertCrypto());
            LoadCryptos();
        }


        private async void LoadCryptos()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync("https://api.coincap.io/v2/assets");
                var cryptoData = JsonConvert.DeserializeObject<CryptoInfo>(response);

                Cryptos.Clear();
                foreach (var crypto in cryptoData.Data)
                {
                    Cryptos.Add(crypto);
                }
            }
        }

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

        private async Task<decimal> GetCryptoPriceInUsd(string cryptoId)
        {
            var response = await _repository.FindCryptoCurrencyAsync(cryptoId);

            return response != null ? response.Data.PriceUsd : 0.0m;
        }
    }
}