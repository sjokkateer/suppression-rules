namespace SuppressionRules.App;

internal class RuleViolationStatisticsDto
{
    public string? Source { get; }

    public IEnumerable<CodeFrequencyDto> Violations { get; }

    public int Total { get; }

    public RuleViolationStatisticsDto(string? source, IEnumerable<CodeFrequencyDto> violations, int total)
    {
        Source = source;
        Violations = violations;
        Total = total;
    }

    internal static RuleViolationStatisticsDto Create(string? source, RuleViolationStatistics ruleViolationStatistics)
    {
        var violations = ruleViolationStatistics.Rules.Select(
            r => new CodeFrequencyDto(r, ruleViolationStatistics.Frequency(r))
        );

        return new(source, violations, ruleViolationStatistics.Total);
    }

    internal static RuleViolationStatisticsDto Example()
    {
        var exampleViolations = new List<CodeFrequencyDto>()
        {
            new("IDE0058", 2),
            new("CA1305", 3),
            new("CA1703", 2),
        };

        return new(
            "some_file_or_directory",
            exampleViolations,
            exampleViolations.Sum(cf => cf.Frequency)
        );
    }
}
