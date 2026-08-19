using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InvoiceGen.Models;
using InvoiceGen.Services;
namespace InvoiceGen
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            LoadSettings();
        }
        private readonly SettingsService settingsService = new SettingsService();

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            string currency = "USD";
            string appearance = "System";

            if (CurrencyComboBox.SelectedItem is ComboBoxItem currencyItem)
            {
                currency = currencyItem.Content.ToString() ?? "USD";
            }

            if (AppearanceComboBox.SelectedItem is ComboBoxItem appearanceItem)
            {
                appearance = appearanceItem.Content.ToString() ?? "System";
            }

            BusinessSettings settings = new BusinessSettings
            {
                BusinessName = BusinessNameTextBox.Text,
                Address = BusinessAddressTextBox.Text,
                Phone = PhoneTextBox.Text,
                Email = EmailTextBox.Text,
                Currency = currency,
                Appearance = appearance
            };

            settingsService.SaveSettings(settings);

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
