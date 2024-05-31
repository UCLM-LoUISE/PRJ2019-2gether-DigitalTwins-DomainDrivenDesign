using ChildHDT.Domain.ValueObjects;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class RoleJsonConverter : JsonConverter<Role>
{
    public override Role Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected string token.");
        }

        string roleName = reader.GetString();
        return RoleConverterHelper.DeserializeRole(roleName);
    }

    public override void Write(Utf8JsonWriter writer, Role value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(RoleConverterHelper.SerializeRole(value));
    }
}
