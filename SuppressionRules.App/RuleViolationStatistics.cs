namespace SuppressionRules.App;

internal class RuleViolationStatistics
{
    private readonly Dictionary<string, int> frequencyTable;

    internal IEnumerable<string> Rules => frequencyTable.Keys;

    internal int Total => frequencyTable.Values.Sum();

    private RuleViolationStatistics()
    {
        frequencyTable = new();
    }

    private RuleViolationStatistics(Dictionary<string, int> frequencyTable)
    {
        this.frequencyTable = frequencyTable;
    }

    internal int Frequency(string rule) => frequencyTable.ContainsKey(rule) ? frequencyTable[rule] : 0;

    internal static RuleViolationStatistics Empty => new();

    internal static RuleViolationStatistics Create(IEnumerable<string> lines)
    {
        var frequencyTable = lines
            .Select(RuleRegex.Get)
            .Where(l => l is not null)
            .Aggregate(new Dictionary<string, int>(), (ft, rule) =>
            {
                if (!ft.ContainsKey(rule!))
                {
                    ft[rule!] = 0;
                }

                ft[rule!]++;

                return ft;
            });

        return new RuleViolationStatistics(frequencyTable);
    }

    internal static RuleViolationStatistics Create(IEnumerable<CodeFrequency> codeFrequencies)
        => new(codeFrequencies.ToDictionary(cf => cf.Code, cf => cf.Frequency));

    internal RuleViolationStatistics Add(RuleViolationStatistics other)
    {
        var newTable = other.frequencyTable
            .Aggregate(frequencyTable, (ft, kvp) =>
            {
                if (!ft.ContainsKey(kvp.Key))
                {
                    ft[kvp.Key] = 0;
                }

                ft[kvp.Key] += kvp.Value;

                return ft;
            });

        return new(newTable);
    }

    internal RuleViolationStatistics Only(IEnumerable<string>? rules)
    {
        if (rules is null || !rules.Any())
        {
            return this;
        }

        var codeFrequencies = rules
            .Where(frequencyTable.ContainsKey)
            .Select(r => new CodeFrequency(r, frequencyTable[r]));

        return Create(codeFrequencies);
    }

    internal RuleViolationStatistics Except(RuleViolationStatistics other)
    {
        var newTable = other.frequencyTable.Keys
            .Where(frequencyTable.ContainsKey)
            .Select(code => new KeyValuePair<string, int>(code, frequencyTable[code] - other.frequencyTable[code]))
            .Where(kvp => kvp.Value > 0)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        return new(newTable);
    }
}
