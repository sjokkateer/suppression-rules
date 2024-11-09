namespace SuppressionRules.App;

internal class RuleViolationStatisticsMapper
{
    internal static RuleViolationStatistics Map(RuleViolationStatisticsDto dto)
        => RuleViolationStatistics.Create(
            dto.Violations.Select(cfDto => new CodeFrequency(cfDto.Code, cfDto.Frequency)));
}
