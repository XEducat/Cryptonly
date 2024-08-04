using Cryptonly._Views;
using Cryptonly.ViewModels;
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

            var frame = App.Current.MainWindow.FindName("mainFrame") as Frame;
            if (frame != null) 
            {
                DataContext = new MainWindowViewModel(frame);
            }
            else
            {
                throw new Exception("Frame on MainWindow not founded");
            }
        }
    }
}