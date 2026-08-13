using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceGen.Models
{
    public class InvoiceItem
    {
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }

        public decimal Total
        {
            get
            {
                return Quantity * PricePerUnit;
            }
        }
    }
}
