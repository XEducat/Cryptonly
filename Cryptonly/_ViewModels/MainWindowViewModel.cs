using Cryptonly._Views;
using Cryptonly.Views;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cryptonly.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly Frame _frame;
        private ICommand _navigateToPopularCryptosCommand;
        private ICommand _navigateToCryptoConverterCommand;
        private ICommand _navigateToSettingsCommand;

        public ICommand NavigateToPopularCryptosCommand => _navigateToPopularCryptosCommand ??= new RelayCommand(NavigateToPopularCryptos);
        public ICommand NavigateToCryptoConverterCommand => _navigateToCryptoConverterCommand ??= new RelayCommand(NavigateToCryptoConverter);
        public ICommand NavigateToSettingsCommand => _navigateToSettingsCommand ??= new RelayCommand(NavigateToSettings);

        public MainWindowViewModel(Frame frame)
        {
            _frame = frame;
            NavigateToPopularCryptos();
        }

        private void NavigateToPopularCryptos()
        {
            _frame?.Navigate(new PopularCryptosPage());
        }

        private void NavigateToCryptoConverter()
        {
            _frame?.Navigate(new CryptoConverterPage());
        }

        private void NavigateToSettings()
        {
            _frame?.Navigate(new SettingsPage());
        }
    }
}
