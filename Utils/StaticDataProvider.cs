using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
    }

}