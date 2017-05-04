using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WatchWord.Infrastructure
{
    public static class ApiJsonSerializer
    {
        private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();

        static ApiJsonSerializer()
        {
            _jsonSerializerSettings.ContractResolver = new LowercaseContractResolver();
        }

        public static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented, _jsonSerializerSettings);
        }
    }

    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}