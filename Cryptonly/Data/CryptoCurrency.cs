using Newtonsoft.Json;

namespace Cryptonly.Data
{
    /// <summary>
    /// Повне представлення для моделі валюти
    /// </summary>
    
    public class CryptoCurrency
    {
        public Data Data { get; set; }
        public long Timestamp { get; set; }
    }

    public class Data
    {
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
            set => supply = Math.Round(value, 5);
        }

        private double? maxSupply;

        [JsonProperty("maxSupply")]
        public double? MaxSupply
        {
            get => maxSupply;
            set => maxSupply = Math.Round(value ?? 0.0, 5);
        }

        private double marketCapUsd;

        [JsonProperty("marketCapUsd")]
        public double MarketCapUsd
        {
            get => marketCapUsd;
            set => marketCapUsd = Math.Round(value, 5);
        }

        private double volumeUsd24Hr;

        [JsonProperty("volumeUsd24Hr")]
        public double VolumeUsd24Hr
        {
            get => volumeUsd24Hr;
            set => volumeUsd24Hr = Math.Round(value, 5);
        }

        private double priceUsd;

        [JsonProperty("priceUsd")]
        public double PriceUsd
        {
            get => priceUsd;
            set => priceUsd = Math.Round(value, 5);
        }

        private double changePercent24Hr;

        [JsonProperty("changePercent24Hr")]
        public double ChangePercent24Hr
        {
            get => changePercent24Hr;
            set => changePercent24Hr = Math.Round(value, 5);
        }

        private double vwap24Hr;

        [JsonProperty("vwap24Hr")]
        public double Vwap24Hr
        {
            get => vwap24Hr;
            set => vwap24Hr = Math.Round(value, 5);
        }

        [JsonProperty("explorer")]
        public string Explorer { get; set; }
    }
}


