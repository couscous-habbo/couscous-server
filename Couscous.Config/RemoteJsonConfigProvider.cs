using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Couscous.Config
{
    public class RemoteJsonConfigProvider : IConfigProvider
    {
        private IDictionary<string, string> _data;
        
        public void Load(string url)
        {
            using var webClient = new WebClient();
            var data = webClient.DownloadString(url);
                
            var jsonObject= JObject.Parse(data);
            var jTokens = jsonObject.Descendants().Where(p => !p.Any());
                
            _data = jTokens.Aggregate(new Dictionary<string, string>(), (properties, jToken) =>
            {
                properties.Add(jToken.Path, jToken.ToString());
                return properties;
            });
        }

        public string GetValueFromKey(string key)
        {
            return !_data.TryGetValue(key, out var value) ? "" : value;
        }
    }
}