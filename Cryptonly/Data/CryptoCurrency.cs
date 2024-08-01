using Newtonsoft.Json;
using System.Globalization;

namespace Cryptonly.Data
{
    /// <summary>
    /// Невелике представлення для моделі валюти,
    /// створенне для оптимізації
    /// </summary>
    public class CryptoCurrency
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        private string priceUsd;

        [JsonProperty("priceUsd")]
        public string PriceUsd
        {
            get => priceUsd;
            set
            {
                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
                {
                    priceUsd = Math.Round(price, 6).ToString();
                }
                else
                {
                    priceUsd = "0.000000";
                }
            }
        }

        private string volumeUsd;

        [JsonProperty("volumeUsd24Hr")]
        public string VolumeUsd
        {
            get => volumeUsd;
            set
            {
                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
                {
                    volumeUsd = Math.Round(price, 6).ToString();
                }
                else
                {
                    volumeUsd = "0.000000";
                }
            }
        }

        public string DisplayName => $"{Name} ({Symbol})";

        //public double Price => double.TryParse(PriceUsd, out var price) ? price : 0;
    }
}
