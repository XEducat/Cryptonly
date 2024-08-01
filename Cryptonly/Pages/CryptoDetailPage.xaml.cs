using Cryptonly.Data;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows;

namespace Cryptonly
{
    //TODO: перенести всі запити в CoincapRepository.cs
    public partial class CryptoDetailPage : Page
    {
        private CryptoCurrencyShort _selectedCrypto;

        public CryptoDetailPage(CryptoCurrencyShort selectedCrypto)
        {
            _selectedCrypto = selectedCrypto;
            InitializeComponent();
            LoadCryptoDetailsAsync("h1"); // Load with default value
        }

        private async void LoadCryptoDetailsAsync(string interval)
        {
            using (HttpClient client = new HttpClient())
            {
                LoadCryptoInfo();
                LoadPriceDiagram(interval);
            }
        }

        // Load detailed data for fields
        private async void LoadCryptoInfo()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync($"https://api.coincap.io/v2/assets/{_selectedCrypto.Id}");
                    var detailedData = JsonConvert.DeserializeObject<CryptoCurrency>(response);
                    DataContext = detailedData.Data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Виникла помилка при завантажнні деталей валюти" + ex.Message);
                }
            }
        }

        // Load historical data for chart
        private async void LoadPriceDiagram(string interval)
        {
            AnalyticData historicalData;

            using (HttpClient client = new HttpClient())
            {
                var historyResponse = await client.GetStringAsync($"https://api.coincap.io/v2/assets/{_selectedCrypto.Id}/history?interval={interval}");
                historicalData = JsonConvert.DeserializeObject<AnalyticData>(historyResponse);
            }

            var lineSeries = new LineSeries
            {
                Title = "Price",
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
                Title = "Аналітика ціни",
                Background = OxyColor.Parse("#F5F5F5"),
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

            // Set the PlotModel to PlotView
            PriceChart.Model = plotModel;
        }

        private void OnIntervalChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IntervalComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                var interval = selectedItem.Tag.ToString();
                LoadCryptoDetailsAsync(interval);
            }
        }
    }
}
