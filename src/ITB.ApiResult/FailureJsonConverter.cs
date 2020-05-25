using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ITB.ResultModel;

namespace ITB.ApiResultModel
{
    // Example of how to implement DerivedTypeJsonConverter
    public class FailureJsonConverter : JsonConverter<Failure>
    {
        protected readonly JsonEncodedText Message = JsonEncodedText.Encode("message");

        public override Failure Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Failure value, JsonSerializerOptions options)
        {
            if (value.GetType() == typeof(Failure))
            {
                writer.WriteStartObject();

                writer.WritePropertyName(Message);

                writer.WriteStringValue(value.Message);

                writer.WriteEndObject();
            }
            else
            {
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
            }
        }
    }
}
