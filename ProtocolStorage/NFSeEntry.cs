using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrazilInvoiceMock.ProtocolStorage
{
    public class NFSeEntry : InvoiceEntry
    {
        public override string DocumentType => "NFS-e";
        public override string KeyValue => ProtocolNumber;
        public string ProtocolNumber { get; set; }
        public string BatchNumber { get; set; }
        public string ReceivalDateTime { get; set; }
        public string CityHallCode { get; set; }
        public string StatusCode { get; set; }
        public string FiscalInvoiceNumber { get; set; }
        public string AuthorizationCode { get; set; }

        public static new NFSeEntry FromLine(string line)
        {
            var parts = line.Split('|');
            if (parts.Length < 8)
                return null;

            return new NFSeEntry
            {
                ProtocolNumber = parts[1],
                BatchNumber = parts[2],
                ReceivalDateTime = parts[3],
                CityHallCode = parts[4],
                StatusCode = parts[5],
                FiscalInvoiceNumber = parts[6],
                AuthorizationCode = parts[7]
            };
        }

        public override string ToLine()
        {
            return $"NFS-e|{ProtocolNumber}|{BatchNumber}|{ReceivalDateTime}|{CityHallCode}|{StatusCode}|{FiscalInvoiceNumber}|{AuthorizationCode}";
        }
    }
}