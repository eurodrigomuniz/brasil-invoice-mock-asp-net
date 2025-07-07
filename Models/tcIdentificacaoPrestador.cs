using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrazilInvoiceMock.Models.NFSe
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    public partial class tcIdentificacaoPrestador
    {

        private string cnpjField;

        private string inscricaoMunicipalField;

        /// <remarks/>
        public string Cnpj
        {
            get
            {
                return this.cnpjField;
            }
            set
            {
                this.cnpjField = value;
            }
        }

        /// <remarks/>
        public string InscricaoMunicipal
        {
            get
            {
                return this.inscricaoMunicipalField;
            }
            set
            {
                this.inscricaoMunicipalField = value;
            }
        }
    }
}