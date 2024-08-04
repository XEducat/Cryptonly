using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace Cryptonly
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserSettings Settings { get; private set; } = new UserSettings();

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

        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoadSettings();
        }

        // Loads user settings from a JSON file and applies them,
        // sets default settings if the settings does not exist.
        private void LoadSettings()
        {
            // Formation of the full path to the settings file
            string basePath = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\net8.0-windows\\", "");
            string settingsFilePath = Path.Combine(basePath, "UserConfig.json");

            if (File.Exists(settingsFilePath))
            {
                var settingsConfig = File.ReadAllText(settingsFilePath);
                var settings = JsonConvert.DeserializeObject<UserSettings>(settingsConfig);

                if (settings != null)
                {
                    SwitchTheme(settings.CurrentTheme);
                    SetLanguage(settings.CurrentLanguage);
                    Settings = settings;
                    return;
                }
            }

            // Set default settings
            SwitchTheme("Light");
            SetLanguage("en-US");
        }

        /// <summary>
        /// Saves the current user settings to a JSON file.
        /// </summary>
        public void SaveSettings()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\net8.0-windows\\", "");
            string settingsFilePath = Path.Combine(basePath, "UserConfig.json");

            File.WriteAllText(settingsFilePath, JsonConvert.SerializeObject(Settings, Formatting.Indented));
        }

        /// <summary>
        /// Switches the application theme to the specified one.
        /// </summary>
        /// <param name="theme">The name of the theme to activate.</param>
        public void SwitchTheme(string? theme)
        {
            if (Settings.CurrentTheme == theme || theme == null) return;

            if (Themes.TryGetValue(theme, out var themeUri))
            {
                Settings.CurrentTheme = theme;
                UpdateResourceDictionary(themeUri, "Themes/");
            }
        }

        /// <summary>
        /// Sets the localization language for the application.
        /// </summary>
        /// <param name="cultureCode">Culture code to select the localization (for example, "uk-UA" for Ukrainian).</param>
        public void SetLanguage(string? cultureCode)
        {
            if (Settings.CurrentLanguage == cultureCode || cultureCode == null) return;

            if (Languages.TryGetValue(cultureCode, out var languageUri))
            {
                Settings.CurrentLanguage = cultureCode;
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