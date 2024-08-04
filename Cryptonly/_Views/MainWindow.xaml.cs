using Cryptonly._Views;
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

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new SettingsPage());
        }
    }
}