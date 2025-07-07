using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrazilInvoiceMock.ProtocolStorage
{
    public class NFeEntry : InvoiceEntry
    {
        public override string DocumentType => "NF-e";
        public override string KeyValue => ReceiptNumber;
        public string ReceiptNumber { get; set; }
        public string AccessKey { get; set; }
        public string ReceivalDateTime { get; set; }
        public string DigestValue { get; set; }
        public string StatusCode { get; set; }
        public string IssuingTypeCode { get; set; }
        public string ProtocolNumber { get; set; }

        public static new NFeEntry FromLine(string line)
        {
            var parts = line.Split('|');
            if (parts.Length < 8)
                return null;

            return new NFeEntry
            {
                ReceiptNumber = parts[1],
                AccessKey = parts[2],
                ReceivalDateTime = parts[3],
                DigestValue = parts[4],
                StatusCode = parts[5],
                IssuingTypeCode = parts[6],
                ProtocolNumber = parts[7]
            };
        }

        public override string ToLine()
        {
            return $"NF-e|{ReceiptNumber}|{AccessKey}|{ReceivalDateTime}|{DigestValue}|{StatusCode}|{IssuingTypeCode}|{ProtocolNumber}";
        }
    }
}