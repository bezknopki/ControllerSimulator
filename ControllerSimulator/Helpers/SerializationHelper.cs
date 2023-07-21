using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerSimulator.Helpers
{
    public static class SerializationHelper
    {
        static JsonSerializerSettings _settings;

        static SerializationHelper()
        {
            DefaultContractResolver contractResolver = new()
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    OverrideSpecifiedNames = false
                }
            };

            _settings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
        }

        public static string Serialize(object obj) 
            => JsonConvert.SerializeObject(obj, _settings);
    }
}
