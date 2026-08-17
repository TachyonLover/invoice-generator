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

namespace InvoiceGen
{

    public partial class MainWindow : Window
    {
        private List<InvoiceItem> invoiceItems = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void AddItem_Click(object sender, RoutedEventArgs e)
        {
            string description = DescriptionTextBox.Text;

            if (!int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            InvoiceItem item = new InvoiceItem
            {
                Description = description,
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
            Invoice invoice = new Invoice
            {
                CustomerName = CustomerNameTextBox.Text,
                CustomerAddress = CustomerAddressTextBox.Text,
                Date = DateTime.Now,
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
                generator.GenerateInvoice(invoice, saveFileDialog.FileName);
            }
        }

    }
}