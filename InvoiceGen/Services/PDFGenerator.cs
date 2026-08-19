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
        public void GenerateInvoice(
    Invoice invoice,
    BusinessSettings settings,
    string filePath)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);

                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Content().Column(column =>
                    {
                        // =========================
                        // BUSINESS HEADER
                        // =========================

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(left =>
                            {
                                left.Item()
                                    .Text(settings.BusinessName)
                                    .FontSize(22)
                                    .Bold();

                                left.Item()
                                    .PaddingTop(4)
                                    .Text(settings.Address);

                                left.Item()
                                    .Text($"{settings.Phone} | {settings.Email}");
                            });

                            row.ConstantItem(230).Column(right =>
                            {
                                right.Item()
                                    .BorderBottom(1)
                                    .PaddingBottom(6)
                                    .Text($"INVOICE #{invoice.InvoiceNumber}")
                                    .FontSize(18)
                                    .Bold();

                                right.Item()
                                    .PaddingTop(8)
                                    .Row(infoRow =>
                                    {
                                        infoRow.RelativeItem()
                                            .Text("Issued");

                                        infoRow.RelativeItem()
                                            .AlignRight()
                                            .Text(invoice.Date.ToString("MM/dd/yyyy"));
                                    });

                                right.Item()
                                    .PaddingTop(6)
                                    .Row(infoRow =>
                                    {
                                        infoRow.RelativeItem()
                                            .Text("Total")
                                            .Bold();

                                        infoRow.RelativeItem()
                                            .AlignRight()
                                            .Text(invoice.Total.ToString("C"))
                                            .Bold();
                                    });
                            });
                        });


                        // =========================
                        // CUSTOMER
                        // =========================

                        column.Item()
                            .PaddingTop(35)
                            .Text("BILL TO")
                            .FontSize(10)
                            .Bold();

                        column.Item()
                            .PaddingTop(6)
                            .Text(invoice.CustomerName)
                            .FontSize(14)
                            .Bold();

                        column.Item()
                            .Text(invoice.CustomerAddress);


                        // =========================
                        // SERVICES TITLE
                        // =========================

                        column.Item()
                            .PaddingTop(35)
                            .PaddingBottom(10)
                            .Text("For Services Rendered")
                            .FontSize(15)
                            .Bold();


                        // =========================
                        // ITEMS TABLE
                        // =========================

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.ConstantColumn(70);
                                columns.ConstantColumn(90);
                                columns.ConstantColumn(90);
                            });

                            table.Header(header =>
                            {
                                header.Cell()
                                    .Background("#222222")
                                    .Padding(8)
                                    .Text("ITEM")
                                    .FontColor("#FFFFFF")
                                    .Bold();

                                header.Cell()
                                    .Background("#222222")
                                    .Padding(8)
                                    .AlignCenter()
                                    .Text("QTY.")
                                    .FontColor("#FFFFFF")
                                    .Bold();

                                header.Cell()
                                    .Background("#222222")
                                    .Padding(8)
                                    .AlignRight()
                                    .Text("UNIT PRICE")
                                    .FontColor("#FFFFFF")
                                    .Bold();

                                header.Cell()
                                    .Background("#222222")
                                    .Padding(8)
                                    .AlignRight()
                                    .Text("TOTAL")
                                    .FontColor("#FFFFFF")
                                    .Bold();
                            });

                            foreach (InvoiceItem item in invoice.Items)
                            {
                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor("#DDDDDD")
                                    .PaddingVertical(10)
                                    .PaddingHorizontal(8)
                                    .Text(item.Description);

                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor("#DDDDDD")
                                    .PaddingVertical(10)
                                    .PaddingHorizontal(8)
                                    .AlignCenter()
                                    .Text(item.Quantity.ToString());

                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor("#DDDDDD")
                                    .PaddingVertical(10)
                                    .PaddingHorizontal(8)
                                    .AlignRight()
                                    .Text(item.PricePerUnit.ToString("C"));

                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor("#DDDDDD")
                                    .PaddingVertical(10)
                                    .PaddingHorizontal(8)
                                    .AlignRight()
                                    .Text(item.Total.ToString("C"));
                            }
                        });


                        // =========================
                        // TOTALS
                        // =========================

                        column.Item()
                            .PaddingTop(25)
                            .AlignRight()
                            .Width(230)
                            .Column(totalColumn =>
                            {
                                totalColumn.Item()
                                    .BorderBottom(1)
                                    .BorderColor("#DDDDDD")
                                    .PaddingVertical(6)
                                    .Row(row =>
                                    {
                                        row.RelativeItem()
                                            .Text("Subtotal");

                                        row.RelativeItem()
                                            .AlignRight()
                                            .Text(invoice.Total.ToString("C"));
                                    });

                                totalColumn.Item()
                                    .PaddingTop(8)
                                    .Row(row =>
                                    {
                                        row.RelativeItem()
                                            .Text("Total")
                                            .FontSize(13)
                                            .Bold();

                                        row.RelativeItem()
                                            .AlignRight()
                                            .Text(invoice.Total.ToString("C"))
                                            .FontSize(13)
                                            .Bold();
                                    });
                            });


                        // =========================
                        // FOOTER MESSAGE
                        // =========================

                        column.Item()
                            .PaddingTop(30)
                            .Text("Thank you for your business!")
                            .FontSize(9)
                            .FontColor("#555555");
                    });
                });
            })
            .GeneratePdf(filePath);
        }
    }
}