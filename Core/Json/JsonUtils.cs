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

        public static bool TrySerialize(object obj, ref string jsonResult, params JsonConverter[] converters)
        {
            if (jsonResult == null)
            {
                jsonResult = JsonConvert.Undefined;
            }
            
            try
            {
                jsonResult = Serialize(obj, converters);
                return true;
            }
            catch (Exception)
            {
                jsonResult = JsonConvert.NaN;
                return false;
            }
        }

        public static T Deserialize<T>(string json, params JsonConverter[] converters)
        {
            Checks.NotNullOrEmpty(json);
            if (json.Equals(JsonConvert.Null)
              ||json.Equals(JsonConvert.NaN)
              ||json.Equals(JsonConvert.Undefined))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(json, CreateDefaultSettings(converters));
        }

        public static bool TryDeserialize<T>(string json, out T result, params JsonConverter[] converters)
        {
            try
            {
                result = Deserialize<T>(json, converters);
                return true;
            }
            catch (Exception)
            {
                // Todo: logo the message
                result = default(T);
                return false;
            }
        }

        public static T Deserialize<T>(string json, Func<T> valueCreator, params JsonConverter[] converters)
        {
            Checks.NotNullOrEmpty(json);
            if (json.Equals(JsonConvert.Null)
             || json.Equals(JsonConvert.NaN)
             || json.Equals(JsonConvert.Undefined))
            {
                return valueCreator();
            }
            return JsonConvert.DeserializeObject<T>(json, CreateDefaultSettings(converters));
        }

        public static bool TryDeserialize<T>(string json, out T result, Func<T> valueCreator, params JsonConverter[] converters)
        {
            try
            {
                result = Deserialize<T>(json, valueCreator, converters);
                return true;
            }
            catch (Exception)
            {
                // Todo: logo the message
                result = valueCreator();
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