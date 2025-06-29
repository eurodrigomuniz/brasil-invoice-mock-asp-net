using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyInvoiceService.ProtocolStorage
{
    public class InvoiceEntry
    {
        public string ReceiptNumber { get; set; }
        public string AccessKey { get; set; }
        public string ReceivalDateTime { get; set; }
        public string DigestValue { get; set; }
        public string StatusCode { get; set; }
        public string IssuingTypeCode { get; set; }
        public string ProtocolNumber { get; set; }

        public static InvoiceEntry FromLine(string line)
        {
            var parts = line.Split('|');
            if (parts.Length < 7)
                return null;

            return new InvoiceEntry
            {
                ReceiptNumber = parts[0],
                AccessKey = parts[1],
                ReceivalDateTime = parts[2],
                DigestValue = parts[3],
                StatusCode = parts[4],
                IssuingTypeCode = parts[5],
                ProtocolNumber = parts[6]
            };
        }

        public string ToLine()
        {
            return $"{ReceiptNumber}|{AccessKey}|{ReceivalDateTime}|{DigestValue}|{StatusCode}|{IssuingTypeCode}|{ProtocolNumber}";
        }
    }
}
