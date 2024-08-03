using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Cryptonly.Data;
using Cryptonly.Services;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Globalization;

namespace Cryptonly.ViewModels
{
    public class CryptoDetailViewModel : BaseViewModel
    {
        private readonly CoinCapRepository _coinCap = new CoinCapRepository();
        private CryptoCurrencyShort _selectedCrypto;

        private string _name;
        private decimal _priceUsd;
        private double _volumeUsd24Hr;
        private double _marketCapUsd;
        private PlotModel _priceChart;
        private ObservableCollection<string> _sellMarkets;
        private ObservableCollection<string> _buyMarkets;
        private string _selectedInterval;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public decimal PriceUsd
        {
            get => _priceUsd;
            set => SetProperty(ref _priceUsd, value);
        }

        public double VolumeUsd24Hr
        {
            get => _volumeUsd24Hr;
            set => SetProperty(ref _volumeUsd24Hr, value);
        }

        public double MarketCapUsd
        {
            get => _marketCapUsd;
            set => SetProperty(ref _marketCapUsd, value);
        }

        public PlotModel PriceChart
        {
            get => _priceChart;
            set => SetProperty(ref _priceChart, value);
        }

        public ObservableCollection<string> SellMarkets
        {
            get => _sellMarkets;
            set => SetProperty(ref _sellMarkets, value);
        }

        public ObservableCollection<string> BuyMarkets
        {
            get => _buyMarkets;
            set => SetProperty(ref _buyMarkets, value);
        }

        public string SelectedInterval
        {
            get => _selectedInterval;
            set
            {
                if (SetProperty(ref _selectedInterval, value))
                {
                    // Load a new chart based on the new interval
                    LoadPriceDiagramAsync(_selectedInterval);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CryptoDetailViewModel(CryptoCurrencyShort selectedCrypto)
        {
            _selectedCrypto = selectedCrypto;
            SellMarkets = new ObservableCollection<string>();
            BuyMarkets = new ObservableCollection<string>();

            SelectedInterval = "h1";
            LoadCryptoInfoAsync();
            LoadPriceDiagramAsync(SelectedInterval);
            LoadMarketInfoAsync();
        }

        private async Task LoadCryptoInfoAsync()
        {
            var detailedData1 = await _coinCap.FindCryptoCurrencyAsync(_selectedCrypto.Id);

            if (detailedData1?.Data != null)
            {
                Name = detailedData1.Data.Name;
                PriceUsd = detailedData1.Data.PriceUsd;
                VolumeUsd24Hr = detailedData1.Data.VolumeUsd24Hr;
                MarketCapUsd = detailedData1.Data.MarketCapUsd;
            }
        }

        private async Task LoadPriceDiagramAsync(string interval)
        {
            var historicalData = await _coinCap.GetPriceHistoryAsync(_selectedCrypto.Id, interval);

            if (historicalData != null)
            {
                var lineSeries = new LineSeries
                {
                    Title = "Ціна",
                    Color = OxyColor.Parse("#6200EE"),
                    StrokeThickness = 2,
                    MarkerSize = 4,
                    MarkerStroke = OxyColors.White,
                    MarkerFill = OxyColor.Parse("#6200EE")
                };

                foreach (var item in historicalData.Data)
                {
                    lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Date), item.PriceUsd));
                }

                var plotModel = new PlotModel
                {
                    Title = "Аналіз ціни",
                    Background = OxyColor.FromArgb(0, 255, 255, 255),
                    TextColor = OxyColor.Parse("#000000")
                };

                plotModel.Series.Add(lineSeries);
                plotModel.Axes.Add(new DateTimeAxis
                {
                    Position = AxisPosition.Bottom,
                    StringFormat = "dd/MM/yyyy",
                    Title = "Дата"
                });

                plotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = "Ціна (USD)",
                    Minimum = 0
                });

                PriceChart = plotModel;
            }
        }

        private async Task LoadMarketInfoAsync()
        {
            var marketsData = await _coinCap.GetMarketDataAsync(_selectedCrypto.Id);

            if (marketsData != null)
            {
                var culture = new CultureInfo("en-US");

                var currencyToSell = marketsData.Data
                    .Where(m => m.QuoteId == _selectedCrypto.Id)
                    .GroupBy(m => m.ExchangeId)
                    .Select(g =>
                    {
                        var minPrice = g.Min(m => m.PriceUsd);
                        return string.Format(culture, "{0} — {1:C5}", g.Key, minPrice);
                    });

                var currencyToBuy = marketsData.Data
                    .Where(m => m.BaseId == _selectedCrypto.Id)
                    .GroupBy(m => m.ExchangeId)
                    .Select(g =>
                    {
                        var minPrice = g.Min(m => m.PriceUsd);
                        return string.Format(culture, "{0} — {1:C5}", g.Key, minPrice);
                    });

                SellMarkets.Clear();
                foreach (var item in currencyToSell)
                {
                    SellMarkets.Add(item);
                }

                BuyMarkets.Clear();
                foreach (var item in currencyToBuy)
                {
                    BuyMarkets.Add(item);
                }
            }
        }
    }
}