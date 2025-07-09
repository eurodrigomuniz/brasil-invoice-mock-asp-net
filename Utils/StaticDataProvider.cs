using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BrazilInvoiceMock.Models;
using Newtonsoft.Json;

namespace BrazilInvoiceMock.Utils
{
    public static class StaticDataProvider
    {
        public static string GetReasonMessage(string code)
        {
            var messages = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/ReasonMessages.json"))
            );

            return messages[code];
        }

        public static string GetAppVersion(string state)
        {
            var version = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/ApplicationVersions.json"))
            );

            return version[state];
        }

        public static NFSeRejection GetNFSeRejection(string code)
        {
            var rejection = JsonConvert.DeserializeObject<Dictionary<string, NFSeRejection>>(
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/NFSeRejections.json"))
                );
            return rejection[code];
        }
    }

}