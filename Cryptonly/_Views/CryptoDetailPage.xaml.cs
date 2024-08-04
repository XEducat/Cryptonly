using System.Windows.Controls;
using Cryptonly.ViewModels;
using Cryptonly.Data;

namespace Cryptonly
{
    public partial class CryptoDetailPage : Page
    {
        public CryptoDetailPage(CryptoShort selectedCrypto)
        {
            InitializeComponent();
            DataContext = new CryptoDetailViewModel(selectedCrypto);
        }
    }
}