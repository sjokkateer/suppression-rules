using Cocona;

namespace SuppressionRules.App;

internal class InvalidRuleViolationStatisticsDtoException : CommandExitedException
{
    internal InvalidRuleViolationStatisticsDtoException(JsonWriter writer, string methodName)
        : base($"{methodName} requires {nameof(RuleViolationStatisticsDto)} objects to operate on. Example: '{writer.Write(RuleViolationStatisticsDto.Example())}'", 1)
    { }
}
