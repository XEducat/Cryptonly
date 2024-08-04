using System.Windows;

namespace Cryptonly.Services
{
    /// <summary>
    /// Provides methods for retrieving localized strings from application resources.
    /// </summary>
    public static class LocalizationHelper
    {
        /// <summary>
        /// Retrieves a localized string from the application resources using the specified key.
        /// </summary>
        /// <param name="key">The key associated with the localized string in the application resources.</param>
        /// <returns>The localized string corresponding to the provided key. Returns null if the key is not found.</returns>
        public static string GetValue(string key)
        {
            return (string)Application.Current.Resources[key];
        }
    }
}
