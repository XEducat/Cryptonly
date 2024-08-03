using System.Configuration;
using System.Data;
using System.Windows;

namespace Cryptonly
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string currentTheme;

        public void SwitchTheme(string theme)
        {
            if (currentTheme == theme) return;

            // Update the current theme
            currentTheme = theme;
            Current.Resources.MergedDictionaries.Clear();

            ResourceDictionary newTheme = new ResourceDictionary();
            switch (theme)
            {
                case "Dark":
                    newTheme.Source = new Uri("/Cryptonly;component/Themes/DarkTheme.xaml", UriKind.Relative);
                    break;
                case "Light":
                    newTheme.Source = new Uri("/Cryptonly;component/Themes/LightTheme.xaml", UriKind.Relative);
                    break;
                default:
                    newTheme.Source = new Uri("/Cryptonly;component/Themes/LightTheme.xaml", UriKind.Relative); // default theme
                    break;
            }

            // Apply the new theme
            Current.Resources.MergedDictionaries.Add(newTheme);
        }
    }

}
