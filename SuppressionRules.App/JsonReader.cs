using System.Text.Json;

namespace SuppressionRules.App;

internal class JsonReader
{
    private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    public RuleViolationStatisticsDto? Read(string input)
    {
        RuleViolationStatisticsDto? output = null;

        try
        {
            output = JsonSerializer.Deserialize<RuleViolationStatisticsDto>(input, options);
        }
        catch (Exception ex) when (ex is JsonException or ArgumentException)
        { }

        return output;
    }
}
