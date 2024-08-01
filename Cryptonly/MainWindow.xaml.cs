using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;
using System.Globalization;
using Cryptonly.Data;
using System.Windows.Controls;
using System.Windows.Input;


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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Закрывает приложение
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Минимизирует окно
        }
    }
}