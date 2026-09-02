using InvoiceGen.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using InvoiceGen.Services;
using InvoiceGen.Converters;

namespace InvoiceGen
{

    public partial class MainWindow : Window
    {
        private List<InvoiceItem> invoiceItems = new();

        public MainWindow()
        {
            InitializeComponent();

            ItemsDataGrid.ItemsSource = invoiceItems;
            InvoiceDatePicker.SelectedDate = DateTime.Today;

            SettingsService settingsService = new SettingsService();
            BusinessSettings settings = settingsService.LoadSettings();

            CurrencyConverter converter =
                (CurrencyConverter)FindResource("CurrencyConverter");

            converter.Currency = settings.Currency;
        }

        private async void AddItem_Click(object sender, RoutedEventArgs e)
        {
            string description = DescriptionTextBox.Text;

            if (string.IsNullOrWhiteSpace(description))
            {
                ItemStatusText.Text = "Enter an item description.";
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
            {
                ItemStatusText.Text = "Invalid quantity.";
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price < 0)
            {
                ItemStatusText.Text = "Invalid price.";
                return;
            }

            InvoiceItem item = new InvoiceItem
            {
                Description = description.Trim(),
                Quantity = quantity,
                PricePerUnit = price
            };

            invoiceItems.Add(item);

            ItemsDataGrid.ItemsSource = null;
            ItemsDataGrid.ItemsSource = invoiceItems;

            DescriptionTextBox.Clear();
            QuantityTextBox.Clear();
            PriceTextBox.Clear();

            ItemStatusText.Text = "Item added successfully!";

            await Task.Delay(2500);

            ItemStatusText.Text = "";
        }

        private void GeneratePdf_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(InvoiceNumberTextBox.Text))
            {
                MessageBox.Show(
                    "Invoice number required.",
                    "Missing Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(CustomerNameTextBox.Text))
            {
                MessageBox.Show(
                    "Customer name required.",
                    "Missing Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (invoiceItems.Count == 0)
            {
                MessageBox.Show(
                    "Add at least one item.",
                    "Missing Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            Invoice invoice = new Invoice
            {
                CustomerName = CustomerNameTextBox.Text,
                CustomerAddress = CustomerAddressTextBox.Text,
                InvoiceNumber = InvoiceNumberTextBox.Text,
                Date = InvoiceDatePicker.SelectedDate ?? DateTime.Today,
                Items = invoiceItems
            };

            Microsoft.Win32.SaveFileDialog saveFileDialog = new()
            {
                FileName = "Invoice",
                DefaultExt = ".pdf",
                Filter = "PDF Documents (*.pdf)|*.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                PdfGenerator generator = new PdfGenerator();
                SettingsService settingsService = new SettingsService();
                BusinessSettings settings = settingsService.LoadSettings();

                generator.GenerateInvoice(
                    invoice,
                    settings,
                    saveFileDialog.FileName
                );
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Owner = this;
            settingsWindow.ShowDialog();

            // Reload settings after the settings window closes
            SettingsService settingsService = new SettingsService();
            BusinessSettings settings = settingsService.LoadSettings();

            CurrencyConverter converter =
                (CurrencyConverter)FindResource("CurrencyConverter");

            converter.Currency = settings.Currency;

            // Force the DataGrid to redraw using the new currency
            ItemsDataGrid.Items.Refresh();
        }

    }
}