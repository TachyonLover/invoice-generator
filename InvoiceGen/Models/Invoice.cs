using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceGen.Models
{
    public class Invoice
    {
        public string InvoiceNumber { get; set; } = "";
        public DateTime Date { get; set; }

        public string CustomerName { get; set; } = "";
        public string CustomerAddress { get; set; } = "";

        public List<InvoiceItem> Items { get; set; } = new();

        public decimal Total
        {
            get
            {
                return Items.Sum(item => item.Total);
            }
        }
    }
}
