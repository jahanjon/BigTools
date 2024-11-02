using Newtonsoft.Json;

namespace Common.CustomAttribute;

public abstract class AppEncryptedJsonConverter<T> : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(T);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return reader.Value == null ? Read(string.Empty) : (object?)Read(reader.Value.ToString());
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(Write((T)value));
    }

    public abstract string Write(T value);

    public abstract T Read(string value);
}