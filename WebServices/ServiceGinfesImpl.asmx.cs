using BrazilInvoiceMock.Models.NFSe;
using BrazilInvoiceMock.Services;
using BrazilInvoiceMock.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace BrazilInvoiceMock.WebServices
{
    /// <summary>
    /// Summary description for ServiceGinfesImpl
    /// </summary>

    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    public class ServiceGinfesImpl : IServiceGinfesImplBinding
    {
        [return: XmlElement("return")]
        public string CancelarNfse(string arg0)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string CancelarNfseV3(string arg0, string arg1)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string CancelarNfseV4(string arg0, string arg1)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarLoteRps(string arg0)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarLoteRpsV3(string arg0, string arg1)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarLoteRpsV4(string arg0, string arg1)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarNfse(string arg0)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarNfsePorRps(string arg0)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarNfsePorRpsV3(string arg0, string arg1)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarNfsePorRpsV4(string arg0, string arg1)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarNfseV3(string arg0, string arg1)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarNfseV4(string arg0, string arg1)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarSituacaoLoteRps(string arg0)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarSituacaoLoteRpsV3(string arg0, string arg1)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string ConsultarSituacaoLoteRpsV4(string arg0, string arg1)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string RecepcionarLoteRps(string arg0)
        {
            throw new NotImplementedException();
        }

        [return: XmlElement("return")]
        public string RecepcionarLoteRpsV3(string arg0, string arg1)
        {
            return new ServiceGinfesImplProcessor(XmlHelper.DeserializeXml<EnviarLoteRpsEnvio>(arg1)).GenerateResponse();
        }

        [return: XmlElement("return")]
        public string RecepcionarLoteRpsV4(string arg0, string arg1)
        {
            throw new NotImplementedException();
        }
    }
}
