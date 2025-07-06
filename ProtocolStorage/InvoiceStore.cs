using MyInvoiceService.ProtocolStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BrazilInvoiceMock.ProtocolStorage
{
    public class InvoiceStore
    {
        private static readonly object _lock = new object();
        private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/invoice-entries.txt");

        public static void SaveEntry(InvoiceEntry entry)
        {
            var line = $"{entry.ReceiptNumber}|{entry.AccessKey}|{entry.ReceivalDateTime}|{entry.DigestValue}|{entry.StatusCode}|{entry.IssuingTypeCode}|{entry.ProtocolNumber}";
            lock (_lock)
            {
                File.AppendAllLines(_filePath, new[] { line });
            }
        }

        public static InvoiceEntry FindByReceipt(string receipt)
        {
            lock (_lock)
            {
                return File.ReadLines(_filePath)
                    .Select(line => InvoiceEntry.FromLine(line))
                    .FirstOrDefault(e => e.ReceiptNumber == receipt);
            }
        }
    }
}