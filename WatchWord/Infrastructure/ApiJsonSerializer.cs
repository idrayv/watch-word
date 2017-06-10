using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace WatchWord.Infrastructure
{
    public static class JsonHelper {
        public static string FirstCharacterToLower(string str)
        {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
                return str;

            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }

    public static class ApiJsonSerializer
    {
        private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();

        static ApiJsonSerializer()
        {
            _jsonSerializerSettings.ContractResolver = new LowercaseContractResolver();
            _jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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
            return JsonHelper.FirstCharacterToLower(propertyName);
        }
    }
}