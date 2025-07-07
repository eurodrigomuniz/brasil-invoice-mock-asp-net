using BrazilInvoiceMock.Models.NFe;
using BrazilInvoiceMock.Services;
using BrazilInvoiceMock.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;

namespace BrazilInvoiceMock.WebServices
{
    /// <summary>
    /// Webservice that receives a request with the protocol number and returns the authorization result for an invoice
    /// </summary>

    public class NFeRetAutorizacao4 : INFeRetAutorizacao4Soap12
    {
        public XmlNode nfeRetAutorizacaoLote(XmlNode nfeDadosMsg)
        {
            return new NFeRetAutorizacao4Processor(XmlHelper.DeserializeXml<TConsReciNFe>(nfeDadosMsg.OuterXml)).GenerateResponse();
        }
    }
}
