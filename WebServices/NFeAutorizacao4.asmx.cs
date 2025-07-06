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
    /// Webservice that receives an invoice request and returns a protocol for assyncronous authorization
    /// </summary>
    public class NFeAutorizacao4 : INFeAutorizacao4Soap12
    {
        public XmlNode nfeAutorizacaoLote(XmlNode nfeDadosMsg)
        {
            return new NFeAutorizacao4Processor(XmlHelper.DeserializeXml<TEnviNFe>(nfeDadosMsg.OuterXml)).GenerateResponse();
        }

        public XmlNode nfeAutorizacaoLoteZip(string nfeDadosMsgZip)
        {
            throw new NotImplementedException();
        }
    }
}
