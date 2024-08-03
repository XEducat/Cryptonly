using System.Windows;

namespace Cryptonly
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string currentTheme;
        private static readonly Dictionary<string, Uri> Themes = new Dictionary<string, Uri>
        {
            { "Dark", new Uri("Themes/DarkTheme.xaml", UriKind.Relative) },
            { "Light", new Uri("Themes/LightTheme.xaml", UriKind.Relative) }
        };

        private static readonly Dictionary<string, Uri> Languages = new Dictionary<string, Uri>
        {
            { "uk-UA", new Uri("Resources/Localization/Strings.uk-UA.xaml", UriKind.Relative) },
            { "en-US", new Uri("Resources/Localization/Strings.en-US.xaml", UriKind.Relative) }
        };

        /// <summary>
        /// Switches the application theme to the specified one.
        /// </summary>
        /// <param name="theme">The name of the theme to activate.</param>
        public void SwitchTheme(string theme)
        {
            if (currentTheme == theme) return;

            currentTheme = theme;
            if (Themes.TryGetValue(theme, out var themeUri))
            {
                UpdateResourceDictionary(themeUri, "Themes/");
            }
        }

        /// <summary>
        /// Sets the localization language for the application.
        /// </summary>
        /// <param name="cultureCode">Culture code to select the localization (for example, "uk-UA" for Ukrainian).</param>
        public void SetLanguage(string cultureCode)
        {
            if (Languages.TryGetValue(cultureCode, out var languageUri))
            {
                UpdateResourceDictionary(languageUri, "Resources/Localization/");
            }
        }

        // Updates the resource dictionary, replacing old dictionaries with new ones.
        private void UpdateResourceDictionary(Uri newUri, string searchTerm)
        {
            RemoveResourceDictionariesContaining(searchTerm);
            var newDictionary = new ResourceDictionary { Source = newUri };
            Current.Resources.MergedDictionaries.Add(newDictionary);
        }

        // Removes resource dictionaries containing the specified search term in their URI.
        private void RemoveResourceDictionariesContaining(string searchTerm)
        {
            for (int i = 0; i < Current.Resources.MergedDictionaries.Count; i++)
            {
                var dictionary = Current.Resources.MergedDictionaries[i];
                if (dictionary.Source != null && dictionary.Source.OriginalString.Contains(searchTerm))
                {
                    Current.Resources.MergedDictionaries.Remove(dictionary);
                    i--;
                }
            }
        }
    }
}