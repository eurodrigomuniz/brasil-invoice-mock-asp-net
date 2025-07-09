using BrazilInvoiceMock.Models;
using BrazilInvoiceMock.Models.NFSe;
using BrazilInvoiceMock.ProtocolStorage;
using BrazilInvoiceMock.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BrazilInvoiceMock.Services
{
    public class ServiceGinfesImplProcessor
    {
        public ServiceGinfesImplProcessor(EnviarLoteRpsEnvio enviarLoteRpsEnvio)
        {
            BatchNumber = enviarLoteRpsEnvio.LoteRps.ListaRps[0].InfRps.IdentificacaoRps.Numero;
            ReceivalDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            ProtocolNumber = GenerateProtocolNumber();
            CityHallCode = enviarLoteRpsEnvio.LoteRps.ListaRps[0].InfRps.Servico.CodigoMunicipio.ToString();
            Discrimination = enviarLoteRpsEnvio.LoteRps.ListaRps[0].InfRps.Servico.Discriminacao;
            StatusCode = ExtractStatusCode();
            if (StatusCode == "S100")
            {
                FiscalInvoiceNumber = GenerateFiscalInvoiceNumber();
                AuthorizationCode = GenerateAuthorizationCode(5);
            }
        }

        public ServiceGinfesImplProcessor(ConsultarLoteRpsEnvio consultarLoteRpsEnvio)
        {
            ProtocolNumber = consultarLoteRpsEnvio.Protocolo;
        }

        private string BatchNumber { get; set; }
        private string ReceivalDateTime { get; set; }
        private string ProtocolNumber { get; set; }
        private string CityHallCode { get; set; }
        private string Discrimination { get; set; }
        private string StatusCode { get; set; }
        private string FiscalInvoiceNumber { get; set; }
        private string AuthorizationCode { get; set; }
        private NFSeEntry InvoiceEntry => InvoiceStore.FindByKeyValue(ProtocolNumber) as NFSeEntry;


        private string GenerateProtocolNumber()
        {
            var random = new Random();
            return string.Concat(Enumerable.Range(0, 8).Select(_ => random.Next(0, 10).ToString()));
        }

        private string GenerateFiscalInvoiceNumber()
        {
            var random = new Random();
            return string.Concat(Enumerable.Range(0, 5).Select(_ => random.Next(0, 10).ToString()));
        }

        private string GenerateAuthorizationCode(int characterLength)
        {
            var random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return "MOCK" + string.Concat(Enumerable.Range(0, characterLength).Select(_ => characters[random.Next(characters.Length)]));
        }

        private string ExtractStatusCode()
        {
            string code = "S100";
            int startIndex = Discrimination.IndexOf('@') + 1;
            int endIndex = Discrimination.IndexOf('*');

            if (startIndex >= 0 && endIndex >= 0)
            {
                return Discrimination.Substring(startIndex, endIndex - startIndex);
            }
            else
            {
                //Logger.Debug($"Desired Status: Any text between braces {{}} was found with in the Discriminacao field, so {code} was defined.");
                return code;
            }
        }

        public string GenerateAuthorizationResponse()
        {
            InvoiceStore.SaveEntry(new NFSeEntry
            {
                ProtocolNumber = ProtocolNumber,
                BatchNumber = BatchNumber,
                ReceivalDateTime = ReceivalDateTime,
                CityHallCode = CityHallCode, // Example code, replace with actual logic
                StatusCode = StatusCode, // Example status code, replace with actual logic
                FiscalInvoiceNumber = FiscalInvoiceNumber, // Example fiscal invoice number, replace with actual logic
                AuthorizationCode = AuthorizationCode // Example authorization code, replace with actual logic
            });

            string authorizationResponse = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/ServiceGinfesImpl_RecepcionarLoteRpsV3.xml"));

            return authorizationResponse.Replace("[NumeroLote]", BatchNumber)
                                                        .Replace("[DataRecebimento]", ReceivalDateTime)
                                                        .Replace("[Protocolo]", ProtocolNumber);
        }

        public string GenerateAuthorizationReturnResponse()
        {
            string authorizationReturnResponse = File.ReadAllText(HttpContext.Current.Server.MapPath($"~/Templates/ServiceGinfesImpl_ConsultarLoteRpsV3_{(InvoiceEntry.StatusCode == "S100" ? 1 : 0)}.xml"));

            if(InvoiceEntry.StatusCode == "S100") 
                return authorizationReturnResponse.Replace("[NumeroNFSe]", InvoiceEntry.FiscalInvoiceNumber)
                .Replace("[CodigoVerificacao]", InvoiceEntry.AuthorizationCode)
                .Replace("[DataEmissao]", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"))
                .Replace("[Numero]", InvoiceEntry.BatchNumber)
                .Replace("[DataEmissaoRps]", DateTime.Now.ToString("yyyy-MM-dd"))
                .Replace("[CodigoMunicipio]", InvoiceEntry.CityHallCode);
            else
            {
                NFSeRejection nfseRejection = StaticDataProvider.GetNFSeRejection(InvoiceEntry.StatusCode);
                return authorizationReturnResponse.Replace("[Codigo]", nfseRejection.Code)
                .Replace("[Mensagem]", nfseRejection.Message)
                .Replace("[Correcao]", nfseRejection.Correction);
            }
        }
    }
}