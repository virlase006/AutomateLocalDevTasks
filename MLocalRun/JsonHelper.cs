using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLocalRun
{
    public class JsonHelper
    {
        public JObject ReadJsonFromFile(string configFile)
        {

            JObject configJson;
            using (StreamReader file = File.OpenText(configFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                configJson = (JObject)serializer.Deserialize(file, typeof(JObject));
            }
            return configJson;
        }

        public bool WriteJsonFile(string configFile, JObject configJson)
        {
            using (StreamWriter file = File.CreateText(configFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, configJson);
            }
            return true;
        }
    }
}
