using Cryptonly.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
                var theme = radioButton.Content.ToString().Contains("Light") ? "Light" : "Dark";
                (Application.Current as App)?.SwitchTheme(theme);
            }
        }
    }
}