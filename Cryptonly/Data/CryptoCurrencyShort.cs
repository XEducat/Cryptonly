using Newtonsoft.Json;

namespace Cryptonly.Data
{
    /// <summary>
    /// Невелике представлення для моделі валюти,
    /// створенне для оптимізації
    /// </summary>
    public class CryptoCurrencyShort
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        private double priceUsd;

        [JsonProperty("priceUsd")]
        public double PriceUsd
        {
            get => priceUsd;
            set => priceUsd = Math.Round(value, 5);
        }

        private double volumeUsd;

        [JsonProperty("volumeUsd24Hr")]
        public double VolumeUsd
        {
            get => volumeUsd;
            set => volumeUsd = Math.Round(value, 5);
        }

        public string DisplayName => $"{Name} ({Symbol})";

        //public double Price => double.TryParse(PriceUsd, out var price) ? price : 0;
    }
}
