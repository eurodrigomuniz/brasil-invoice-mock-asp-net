using BrazilInvoiceMock.ProtocolStorage;
using BrazilInvoiceMock.Utils;
using MyInvoiceService.ProtocolStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace BrazilInvoiceMock.Services
{
    public class AuthorizationProcessor
    {
        public AuthorizationProcessor(TEnviNFe enviNFe)
        {
            AcessKey = enviNFe.NFe[0].infNFe.Id.Replace("NFe", "");
            StatusCode = ExtractStatusCode(enviNFe);
            IssuingTypeCode = enviNFe.NFe[0].infNFe.ide.tpEmis;
            StateCode = enviNFe.NFe[0].infNFe.ide.cUF;
            DigestValue = enviNFe.NFe[0].Signature.SignedInfo.Reference.DigestValue;
            ReceiptNumber = GenerateReceiptNumber();
            ReceivalDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            ProtocolNumber = StatusCode == "100" ? GenerateProtocolNumber() : string.Empty;
        }

        private string AcessKey { get; set; }
        private string StatusCode { get; set; }

        private string IssuingTypeCode { get; set; }
        private string StateCode { get; set; }
        private string DigestValue { get; set; }
        private string ReceiptNumber { get; set; }
        private string ReceivalDateTime { get; set; }
        private string ProtocolNumber { get; set; }

        private string ExtractStatusCode(TEnviNFe enviNFe)
        {
            // Default status code
            int cStat = 100;

            var obsContText = enviNFe.NFe[0].infNFe?.infAdic?.obsCont?.FirstOrDefault()?.xTexto;
            if (!string.IsNullOrEmpty(obsContText) && int.TryParse(obsContText, out int validCStat))
            {
                cStat = validCStat;
            }

            return cStat.ToString();
        }

        private string GenerateReceiptNumber()
        {
            var random = new Random();
            return $"{StateCode}1{string.Concat(Enumerable.Range(0, 12).Select(_ => random.Next(0, 10).ToString()))}";
        }

        private string GenerateProtocolNumber()
        {
            var random = new Random();
            return $"1{StateCode}{DateTime.Now:yy}{string.Concat(Enumerable.Range(0, 10).Select(_ => random.Next(0, 10).ToString()))}";
        }

        public XmlNode GenerateAuthorizationResponse()
        {
            var entry = new InvoiceEntry
            {
                ReceiptNumber = ReceiptNumber,
                AccessKey = AcessKey,
                ReceivalDateTime = ReceivalDateTime,
                DigestValue = DigestValue,
                StatusCode = StatusCode,
                IssuingTypeCode = IssuingTypeCode,
                ProtocolNumber = ProtocolNumber
            };

            InvoiceStore.SaveEntry(entry);

            string authorizationResponse = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/AuthorizationResponse.xml"));

            string receivalStatusCode = "103";

            return new XmlDocument
            {
                InnerXml = authorizationResponse.Replace("[verAplic]", StaticDataProvider.GetAppVersion(StateCode))
                                                         .Replace("[cStat]", receivalStatusCode)
                                                         .Replace("[xMotivo]", StaticDataProvider.GetReasonMessage(receivalStatusCode))
                                                         .Replace("[cUF]", StateCode)
                                                         .Replace("[dhRecbto]", ReceivalDateTime)
                                                         .Replace("[nRec]", ReceiptNumber)
            };
        }
    }
}