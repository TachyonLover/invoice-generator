using System.Windows;
using System.Windows.Controls;
using InvoiceGen.Models;
using InvoiceGen.Services;


namespace InvoiceGen
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {

        private readonly SettingsService settingsService = new SettingsService();

        public SettingsWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            string currency = "USD";
            string appearance = "Light";

            if (CurrencyComboBox.SelectedItem is ComboBoxItem currencyItem)
            {
                currency = currencyItem.Content.ToString() ?? "USD";
            }

            if (AppearanceComboBox.SelectedItem is ComboBoxItem appearanceItem)
            {
                appearance = appearanceItem.Content.ToString() ?? "Light";
            }

            BusinessSettings settings = new BusinessSettings
            {
                BusinessName = BusinessNameTextBox.Text.Trim(),
                Address = BusinessAddressTextBox.Text.Trim(),
                Phone = PhoneTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
                Currency = currency,
                Appearance = appearance
            };

            settingsService.SaveSettings(settings);
            ThemeService themeService = new ThemeService();
            themeService.ApplyTheme(settings.Appearance);

            Close();
        }

        private void LoadSettings()
        {
            BusinessSettings settings = settingsService.LoadSettings();

            BusinessNameTextBox.Text = settings.BusinessName;
            BusinessAddressTextBox.Text = settings.Address;
            PhoneTextBox.Text = settings.Phone;
            EmailTextBox.Text = settings.Email;

            foreach (ComboBoxItem item in CurrencyComboBox.Items)
            {
                if (item.Content.ToString() == settings.Currency)
                {
                    CurrencyComboBox.SelectedItem = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in AppearanceComboBox.Items)
            {
                if (item.Content.ToString() == settings.Appearance)
                {
                    AppearanceComboBox.SelectedItem = item;
                    break;
                }
            }
        }

    }
}
