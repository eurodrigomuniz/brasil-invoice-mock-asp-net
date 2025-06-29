using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrazilInvoiceMock.Utils
{
    public class XmlHelper
    {
        public static T DeserializeXml<T>(string xmlString)
        {
            if (string.IsNullOrEmpty(xmlString))
                throw new ArgumentNullException(nameof(xmlString), "XML string cannot be null or empty.");
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var reader = new System.IO.StringReader(xmlString))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}