using Cryptonly.Data;
using Newtonsoft.Json;
using System.Net.Http;

namespace Cryptonly.Services
{
    public class CoinCapRepository
    {   
        public event EventHandler<ErrorEventArgs> ErrorOccurred; // Подія для повідомлення про помилки

        private static readonly HttpClient Client = new HttpClient();

        protected virtual void OnErrorOccurred(string methodName, string message)
        {
            ErrorOccurred?.Invoke(this, new ErrorEventArgs(methodName, message));
        }

        public async Task<CryptoInfo?> GetAllCryptoCurrenciesAsync()
        {
            try
            {
                var response = await Client.GetStringAsync("https://api.coincap.io/v2/assets");
                return JsonConvert.DeserializeObject<CryptoInfo>(response);
            }
            catch (HttpRequestException httpEx)
            {
                OnErrorOccurred(nameof(GetAllCryptoCurrenciesAsync), $"Помилка при запиті даних про криптовалюти: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                OnErrorOccurred(nameof(GetAllCryptoCurrenciesAsync), $"Помилка при обробці даних про криптовалюти: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                OnErrorOccurred(nameof(GetAllCryptoCurrenciesAsync), $"Невідома помилка при завантаженні даних про криптовалюти: {ex.Message}");
            }

            return null;
        }

        public async Task<CryptoCurrency?> FindCryptoCurrencyAsync(string id)
        {
            try
            {
                var response = await Client.GetStringAsync($"https://api.coincap.io/v2/assets/{id}");
                return JsonConvert.DeserializeObject<CryptoCurrency>(response);
            }
            catch (HttpRequestException httpEx)
            {
                OnErrorOccurred(nameof(FindCryptoCurrencyAsync), $"Помилка при запиті даних про криптовалюту: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                OnErrorOccurred(nameof(FindCryptoCurrencyAsync), $"Помилка при обробці даних про криптовалюту: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                OnErrorOccurred(nameof(FindCryptoCurrencyAsync), $"Невідома помилка при завантаженні даних про криптовалюту: {ex.Message}");
            }

            return null;
        }

        public async Task<AnalyticData?> GetPriceHistoryAsync(string id, string interval)
        {
            try
            {
                var response = await Client.GetStringAsync($"https://api.coincap.io/v2/assets/{id}/history?interval={interval}");
                return JsonConvert.DeserializeObject<AnalyticData>(response);
            }
            catch (HttpRequestException httpEx)
            {
                OnErrorOccurred(nameof(GetPriceHistoryAsync), $"Помилка при запиті даних про графік: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                OnErrorOccurred(nameof(GetPriceHistoryAsync), $"Помилка при обробці даних про графік: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                OnErrorOccurred(nameof(GetPriceHistoryAsync), $"Невідома помилка при завантаженні графіка: {ex.Message}");
            }

            return null;
        }

        public async Task<MarketData?> GetMarketDataAsync(string id)
        {
            try
            {
                var response = await Client.GetStringAsync($"https://api.coincap.io/v2/assets/{id}/markets");
                return JsonConvert.DeserializeObject<MarketData>(response);
            }
            catch (HttpRequestException httpEx)
            {
                OnErrorOccurred(nameof(GetMarketDataAsync), $"Помилка при запиті ринкової інформації: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                OnErrorOccurred(nameof(GetMarketDataAsync), $"Помилка при обробці ринкової інформації: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                OnErrorOccurred(nameof(GetMarketDataAsync), $"Невідома помилка при завантаженні ринкової інформації: {ex.Message}");
            }

            return null;
        }
    }
}

public class ErrorEventArgs : EventArgs
{
    public string MethodName { get; }
    public string Message { get; }

    public ErrorEventArgs(string methodName, string message)
    {
        MethodName = methodName;
        Message = message;
    }
}
