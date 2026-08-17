using InvoiceGen.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceGen.Services
{
    public class PdfGenerator
    {
        public void GenerateInvoice(Invoice invoice, string filePath)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);

                    page.Content().Column(column =>
                    {
                        column.Item()
                            .Text("INVOICE")
                            .FontSize(24)
                            .Bold();

                        column.Item()
                            .Text($"Customer: {invoice.CustomerName}");

                        column.Item()
                            .Text($"Address: {invoice.CustomerAddress}");

                        column.Item().PaddingTop(20).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(80);
                                columns.ConstantColumn(100);
                                columns.ConstantColumn(100);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Item").Bold();
                                header.Cell().Text("Quantity").Bold();
                                header.Cell().Text("Unit Price").Bold();
                                header.Cell().Text("Total").Bold();
                            });

                            foreach (InvoiceItem item in invoice.Items)
                            {
                                table.Cell().Text(item.Description);
                                table.Cell().Text(item.Quantity.ToString());
                                table.Cell().Text(item.PricePerUnit.ToString("C"));
                                table.Cell().Text(item.Total.ToString("C"));
                            }
                        });

                        column.Item()
                            .PaddingTop(20)
                            .AlignRight()
                            .Text($"Total: {invoice.Total:C}")
                            .Bold();
                    });
                });
            })
            .GeneratePdf(filePath);
        }
    }
}