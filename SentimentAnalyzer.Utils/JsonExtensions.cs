using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SentimentAnalyser.Utils
{
    public static class JsonExtensions
    {
        private static readonly string EmptyJson = "{}";

        public static string ToJSON(this object Object)
        {
            return JsonConvert.SerializeObject(Object, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });
        }

        public static T ToObject<T>(this string JSON, T DefaultValue, JsonSerializerSettings settings = null)
        {
            try
            {
                return JSON.IsEmpty()
                    ? DefaultValue
                    : (T) JsonConvert.DeserializeObject(JSON, typeof(T), settings);
            }
            catch
            {
                return DefaultValue;
            }
        }

        public static dynamic ToObject(this string JSON)
        {
            try
            {
                return JSON.IsEmpty() ? null : JObject.Parse(JSON);
            }
            catch
            {
                return null;
            }
        }

        public static T ToObject<T>(this string json)
        {
            try
            {
                return json.IsEmpty()
                    ? default
                    : (T) JsonConvert.DeserializeObject(json, typeof(T));
            }
            catch
            {
                return default;
            }
        }

        public static T Clone<T>(this T source)
        {
            return source.ToJSON().ToObject<T>();
        }

        public static T Convert<T>(this object source)
        {
            return source.ToJSON().ToObject<T>();
        }

        public static object Populate(this string values, object model)
        {
            JsonConvert.PopulateObject(values, model);
            return model;
        }

        public static T Populate<T>(this string values)
        {
            var model = EmptyJson.ToObject<T>();
            JsonConvert.PopulateObject(values, model);
            return model;
        }
    }
}