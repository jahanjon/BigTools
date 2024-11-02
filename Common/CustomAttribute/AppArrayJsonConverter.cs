using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.CustomAttribute;

public abstract class ArrayJsonConverter<T> : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(List<T>);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var list = new List<T>();
        if (reader.TokenType == JsonToken.StartArray)
        {
            foreach (var item in JToken.Load(reader).Children())
            {
                list.Add(Read(item.ToString()));
            }
        }

        return list;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        new List<string>();
        writer.WriteStartArray();
        foreach (var item in (List<T>)value)
        {
            var text = Write(item);
            if (text.StartsWith("{") || text.StartsWith("["))
            {
                writer.WriteRawValue(text);
            }
            else
            {
                writer.WriteValue(text);
            }
        }

        writer.WriteEndArray();
    }

    public abstract string Write(T value);

    public abstract T Read(string value);
}