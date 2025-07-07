using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrazilInvoiceMock.ProtocolStorage
{
    public abstract class InvoiceEntry
    {
        public abstract string DocumentType { get; }
        public abstract string KeyValue { get; }
        public abstract string ToLine();
        public static InvoiceEntry FromLine(string line)
        {
            var parts = line.Split('|');
            
            if (parts[0] == "NF-e")
                return NFeEntry.FromLine(line);
            else if (parts[0] == "NFS-e")
                return NFSeEntry.FromLine(line);
            else
                throw new InvalidOperationException("Unknown entry type.");
        }
    }
}
