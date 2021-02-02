using System;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Library.Common.Types.JsonConverters.Newtonsoft
{
    public sealed class JsonDocumentConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is JsonDocument jsonDoc))
                return;

            var stringJson = jsonDoc.RootElement.GetRawText();

            var jObject = JObject.Parse(stringJson);

            using var jReader = jObject.Root.CreateReader();

            writer.WriteToken(jReader);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(JsonDocument);
        }
    }
}
