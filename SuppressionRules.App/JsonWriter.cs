using System.Text.Json;

namespace SuppressionRules.App;

internal class JsonWriter
{
    public string Write(RuleViolationStatisticsDto ruleViolationStatisticsDto)
    {
        return JsonSerializer.Serialize(ruleViolationStatisticsDto);
    }
}
