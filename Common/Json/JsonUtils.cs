namespace Common.Json
{

    public sealed class JsonUtils
    {

        public static T ReadJsonString<T>(string value)
        {
            return (T) ReadJsonString(value);
        }

        public static object ReadJsonString(string value)
        {
            return null;
        }

        public static void WriteJsonObject<T>(T obj)
        {

        }

    }

}