using Cryptonly.Views;
using System.Windows;
using System.Windows.Controls;

namespace Cryptonly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateToPopularCryptos();
        }

        private void NavigateToPopularCryptos()
        {
            mainFrame.Navigate(new PopularCryptosPage());
        }

        private void CryptonlyButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPopularCryptos();
        }

        private void CryptoConverterButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new CryptoConverterPage());
        }

        private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var resourceKey = radioButton.Content.ToString();
                if (resourceKey == FindResource("LightTheme").ToString())
                {
                    (Application.Current as App)?.SwitchTheme("Light");
                }
                else if (resourceKey == FindResource("DarkTheme").ToString())
                {
                    (Application.Current as App)?.SwitchTheme("Dark");
                }
            }
        }

        private void OnLanguageRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                var languageKey = radioButton.Content.ToString();
                if (languageKey == FindResource("EnglishLanguageText").ToString())
                {
                    (Application.Current as App)?.SetLanguage("en-US");
                }
                else if (languageKey == FindResource("UkrainianLanguageText").ToString())
                {
                    (Application.Current as App)?.SetLanguage("uk-UA");
                }
            }
        }
    }
}