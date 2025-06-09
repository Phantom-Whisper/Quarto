using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Manager;
using Model;

namespace Model
{
    public class PlayerConverter : JsonConverter<IPlayer>
    {
        public override IPlayer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var jsonObject = doc.RootElement;

            if (!jsonObject.TryGetProperty("Name", out var nameProp))
                throw new JsonException("Missing Name property");

            var name = nameProp.GetString() ?? throw new JsonException("Name is null");

            if (jsonObject.TryGetProperty("PlayerType", out var typeProp))
            {
                var playerType = typeProp.GetString();
                if (playerType == "HumanPlayer")
                    return new HumanPlayer(name);
                else if (playerType == "DumbAIPlayer")
                    return new DumbAIPlayer();
                else
                    throw new JsonException($"Unknown player type {playerType}");
            }
            else
            {
                return new HumanPlayer(name);
            }
        }

        public override void Write(Utf8JsonWriter writer, IPlayer value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("Name", value.Name);

            string playerType = value switch
            {
                HumanPlayer => "HumanPlayer",
                DumbAIPlayer => "DumbAIPlayer",
                _ => "Unknown"
            };
            writer.WriteString("PlayerType", playerType);

            writer.WriteEndObject();
        }
    }
}