using Newtonsoft.Json;

namespace Cryptonly.Data
{
    /// <summary>
    /// Small model for currency,
    /// created for optimization
    /// </summary>
    public class CryptoShort
    {
        private const int DECIMAL_LIMIT = 6;

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
            set => priceUsd = Math.Round(value, DECIMAL_LIMIT);
        }

        private double volumeUsd;

        [JsonProperty("volumeUsd24Hr")]
        public double VolumeUsd
        {
            get => volumeUsd;
            set => volumeUsd = Math.Round(value, DECIMAL_LIMIT);
        }

        public string DisplayName => $"{Name} ({Symbol})";
    }
}
