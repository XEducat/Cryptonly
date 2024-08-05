using System.Windows;

namespace Cryptonly.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        // Retrieves the available languages from the application settings.
        public IEnumerable<LanguageInfo> Languages => App.GetLanguages();

        private bool _isLightTheme;
        private bool _isDarkTheme;
        private string _selectedLanguage;

        public LanguageInfo SelectedLanguage
        {
            get => App.GetLanguageByCode(_selectedLanguage);
            set
            {
                if (SetProperty(ref _selectedLanguage, value.Code))
                {
                    var app = Application.Current as App;
                    app?.SetLanguage(value.Code);
                    app?.SaveSettings();
                }
            }
        }

        public bool IsLightTheme
        {
            get => _isLightTheme;
            set
            {
                if (SetProperty(ref _isLightTheme, value) && value)
                {
                    var app = Application.Current as App;
                    app?.SwitchTheme("Light");
                    IsDarkTheme = !value;
                    app?.SaveSettings();
                }
            }
        }

        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value) && value)
                {
                    var app = Application.Current as App;
                    app?.SwitchTheme("Dark");
                    IsLightTheme = !value;
                    app?.SaveSettings();
                }
            }
        }

        /// <summary>
        /// Initializes the settings view model with the current application settings or default values.
        /// </summary>
        public SettingsViewModel()
        {
            var currentSettings = App.Settings;
            if (currentSettings == null)
            {
                // Default settings
                IsLightTheme = true;
                SelectedLanguage = App.GetLanguageByCode("en-US"); 
            }
            else
            {
                IsLightTheme = currentSettings.CurrentTheme == "Light";
                IsDarkTheme = currentSettings.CurrentTheme == "Dark";
                SelectedLanguage = App.GetLanguageByCode(currentSettings.CurrentLanguage);
            }
        }
    }
}