using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;

namespace Core.Json
{

    public sealed class JsonUtils
    {
               
        public static string Serialize(object obj, params JsonConverter[] converters)
        {
            return obj == null 
                 ? JsonConvert.Null
                 : JsonConvert.SerializeObject(obj, CreateDefaultSettings(converters))
                 ;
        }

        public static bool TrySerialize(object obj, ref string result, params JsonConverter[] converters)
        {
            try
            {
                result = Serialize(obj, converters);
                return true;
            }
            catch (Exception)
            {
                result = JsonConvert.Null;
                return false;
            }
        }

        public static T Deserialize<T>(string json, params JsonConverter[] converters)
        {
            Checks.NotNullOrEmpty(json);
            if (json.Equals(JsonConvert.Null))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(json, CreateDefaultSettings(converters));
        }

        public static bool TryDeserialize<T>(string json, out T result, Func<T> defaultFactory, params JsonConverter[] converters)
        {
            try
            {
                result = Deserialize<T>(json, converters);
                return true;
            }
            catch (Exception)
            {
                // Todo: logo the message
                result = defaultFactory();
                return false;
            }
        }

        private static JsonSerializerSettings CreateDefaultSettings(JsonConverter[] converters)
        {
            return new JsonSerializerSettings()
            {
                   DateFormatHandling   = DateFormatHandling.IsoDateFormat,
                   Converters           = converters,
                   NullValueHandling    = NullValueHandling.Include,
                   StringEscapeHandling = StringEscapeHandling.Default
            };
        }

    }

}