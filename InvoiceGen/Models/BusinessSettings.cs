using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceGen.Models
{
    public class BusinessSettings
    {
        public string BusinessName { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";

        public string Currency { get; set; } = "USD";
        public string Appearance { get; set; } = "System";
    }
}