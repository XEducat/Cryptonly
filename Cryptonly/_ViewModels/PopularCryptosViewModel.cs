using System.Collections.ObjectModel;
using System.Windows.Input;
using Cryptonly.Data;
using Cryptonly.Services;

namespace Cryptonly.ViewModels
{
    public class PopularCryptosViewModel : BaseViewModel
    {
        private readonly CoinCapRepository _coinCap = new CoinCapRepository();
        private ObservableCollection<CryptoShort> _cryptos;
        private string _searchText;
        public ICommand SearchCommand { get; }

        public ObservableCollection<CryptoShort> Cryptos
        {
            get { return _cryptos; }
            set { SetProperty(ref _cryptos, value); }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    SearchCryptos();
                }
            }
        }


        public PopularCryptosViewModel()
        {
            SearchCommand = new RelayCommand(async () => await SearchCryptos());
            LoadCryptos();
        }

        /// <summary>
        /// Loading info about all cryptocurrencies
        /// </summary>
        private async Task LoadCryptos()
        {
            var cryptoData = await _coinCap.GetAllCryptoCurrenciesAsync();

            if (cryptoData != null)
                Cryptos = new ObservableCollection<CryptoShort>(cryptoData.Data);
        }

        /// <summary>
        /// Filters the cryptocurrency list based on the search text. Loads all cryptocurrencies if the search text is empty.
        /// </summary>
        private async Task SearchCryptos()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadCryptos();
            }
            else
            {
                var filtered = _cryptos.Where(c => c.DisplayName.ToLower().Contains(SearchText.ToLower()));
                Cryptos = new ObservableCollection<CryptoShort>(filtered);
            }
        }
    }
}