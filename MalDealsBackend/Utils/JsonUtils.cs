using System.Text.Json;
using System.Text.Json.Serialization;

namespace MalDeals.Utils;
public static class JsonUtils
{
    public static readonly JsonSerializerOptions DefaultOptions = new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    public static string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj, DefaultOptions);
    }
}