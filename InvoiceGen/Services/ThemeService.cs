using System.Windows;

namespace InvoiceGen.Services
{
    public class ThemeService
    {
        public void ApplyTheme(string appearance)
        {
            string themePath;

            if (appearance == "Dark")
            {
                themePath = "Themes/DarkTheme.xaml";
            }
            else
            {
                themePath = "Themes/LightTheme.xaml";
            }

            ResourceDictionary theme = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(theme);
        }
    }
}