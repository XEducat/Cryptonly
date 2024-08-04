using Newtonsoft.Json;

namespace Cryptonly.Data
{
    public class CryptoDetails
    {
        private const int DECIMAL_LIMIT = 6;

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        private double supply;

        [JsonProperty("supply")]
        public double Supply
        {
            get => supply;
            set => supply = Math.Round(value, DECIMAL_LIMIT);
        }

        private double? maxSupply;

        [JsonProperty("maxSupply")]
        public double? MaxSupply
        {
            get => maxSupply;
            set => maxSupply = Math.Round(value ?? 0.0, DECIMAL_LIMIT);
        }

        private double marketCapUsd;

        [JsonProperty("marketCapUsd")]
        public double MarketCapUsd
        {
            get => marketCapUsd;
            set => marketCapUsd = Math.Round(value, DECIMAL_LIMIT);
        }

        private double volumeUsd24Hr;

        [JsonProperty("volumeUsd24Hr")]
        public double VolumeUsd24Hr
        {
            get => volumeUsd24Hr;
            set => volumeUsd24Hr = Math.Round(value, DECIMAL_LIMIT);
        }

        private decimal priceUsd;

        [JsonProperty("priceUsd")]
        public decimal PriceUsd
        {
            get => priceUsd;
            set => priceUsd = Math.Round(value, DECIMAL_LIMIT);
        }

        private double changePercent24Hr;

        [JsonProperty("changePercent24Hr")]
        public double ChangePercent24Hr
        {
            get => changePercent24Hr;
            set => changePercent24Hr = Math.Round(value, DECIMAL_LIMIT);
        }

        private double vwap24Hr;

        [JsonProperty("vwap24Hr")]
        public double Vwap24Hr
        {
            get => vwap24Hr;
            set => vwap24Hr = Math.Round(value, DECIMAL_LIMIT);
        }

        [JsonProperty("explorer")]
        public string Explorer { get; set; }
    }
}


