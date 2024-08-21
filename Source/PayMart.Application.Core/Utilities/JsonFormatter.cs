using System.Text.Json;

namespace PayMart.Application.Core.NovaPasta;

public class JsonFormatter
{
    public static string Formatter(string json)
    {
        var jsonDocument = JsonDocument.Parse(json);
        var formattedJson = JsonSerializer.Serialize(jsonDocument.RootElement, new JsonSerializerOptions { WriteIndented = true });

        return formattedJson;
    }

}
