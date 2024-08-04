using Cryptonly.Services;
using System.Windows;

namespace Cryptonly.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private bool _isLightTheme;
        private bool _isDarkTheme;
        private bool _isEnglishLanguage;
        private bool _isUkrainianLanguage;

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

        public bool IsEnglishLanguage
        {
            get => _isEnglishLanguage;
            set
            {
                if (SetProperty(ref _isEnglishLanguage, value) && value)
                {
                    var app = Application.Current as App;
                    app?.SetLanguage("en-US");
                    IsUkrainianLanguage = !value;
                    app?.SaveSettings();
                }
            }
        }

        public bool IsUkrainianLanguage
        {
            get => _isUkrainianLanguage;
            set
            {
                if (SetProperty(ref _isUkrainianLanguage, value) && value)
                {
                    var app = Application.Current as App;
                    app?.SetLanguage("uk-UA");
                    IsEnglishLanguage = !value;
                    app?.SaveSettings();
                }
            }
        }

        public SettingsViewModel()
        {
            if (App.Settings == null)
            {
                IsLightTheme = true; // Или другая логика определения текущей темы
                IsEnglishLanguage = true; // Или другая логика определения текущего языка
            }
            else
            {
                // Установка значений на основе текущих настроек приложения
                IsLightTheme = App.Settings.CurrentTheme == "Light";
                IsDarkTheme = App.Settings.CurrentTheme == "Dark";
                IsEnglishLanguage = App.Settings.CurrentLanguage == "en-US";
                IsUkrainianLanguage = App.Settings.CurrentLanguage == "uk-UA";
            }
        }
    }
}