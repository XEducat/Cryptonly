using Newtonsoft.Json;

namespace Cryptonly.Data
{
    /// <summary>
    /// Small model for currency,
    /// created for optimization
    /// </summary>
    public class CryptoCurrencyShort
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("explorer")]
        public string Explorer { get; set; }

        private decimal priceUsd;

        [JsonProperty("priceUsd")]
        public decimal PriceUsd
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
    }
}
