using BrazilInvoiceMock.ProtocolStorage;
using BrazilInvoiceMock.Utils;
using MyInvoiceService.ProtocolStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Xml;

namespace BrazilInvoiceMock.Services
{
    public class NFeRetAutorizacao4Processor
    {
        public NFeRetAutorizacao4Processor(TConsReciNFe consReciNFe)
        {
            ReceiptNumber = consReciNFe.nRec;
        }
        private string ReceiptNumber { get; set; }
        private InvoiceEntry InvoiceEntry => InvoiceStore.FindByReceipt(ReceiptNumber);

        public XmlNode GenerateResponse()
        {
            string authorizationReturnResponse = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/NFeRetAutorizacao4.xml"));

            authorizationReturnResponse = authorizationReturnResponse.Replace("[verAplic]", StaticDataProvider.GetAppVersion(ReceiptNumber.Substring(0, 2)))
            .Replace("[nRec]", ReceiptNumber)
            .Replace("[cUF]", ReceiptNumber.Substring(0, 2))
            .Replace("[dhRecbto]", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"))
            .Replace("[chNFe]", InvoiceEntry.AccessKey)
            .Replace("[receivalDate]", InvoiceEntry.ReceivalDateTime)
            .Replace("[digVal]", InvoiceEntry.DigestValue)
            .Replace("[cStat]", InvoiceEntry.StatusCode)
            .Replace("[nProt]", InvoiceEntry.ProtocolNumber)
            .Replace("[xMotivo]", StaticDataProvider.GetReasonMessage(InvoiceEntry.StatusCode));

            if (InvoiceEntry.StatusCode != "100") return RemoveTags(authorizationReturnResponse, "nProt", "digVal");
            else return new XmlDocument{ InnerXml = authorizationReturnResponse };
        }

        private XmlNode RemoveTags(string xmlDocument, params string[] tagNames)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlDocument);

            foreach (var tagName in tagNames)
            {
                var nodes = xml.GetElementsByTagName(tagName);
                foreach (XmlNode node in nodes.Cast<XmlNode>().ToList())
                {
                    node.ParentNode.RemoveChild(node);
                }
            }

            return xml;
        }
    }
}