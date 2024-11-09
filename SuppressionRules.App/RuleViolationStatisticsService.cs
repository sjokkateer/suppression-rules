namespace SuppressionRules.App;

internal class RuleViolationStatisticsService
{
    internal RuleViolationStatistics Get(FileInfo file)
    {
        if (!file.Exists) throw new ArgumentException($"The file '{file.FullName}' does not exist.");

        return RuleViolationStatistics.Create(new SuppressionFile(file).GetLines());
    }

    internal RuleViolationStatistics Get(DirectoryInfo directory)
    {
        if (!directory.Exists) throw new ArgumentException($"The directory '{directory.FullName}' does not exist.");

        var enumerationOptions = new EnumerationOptions()
        {
            MatchCasing = MatchCasing.CaseInsensitive,
            RecurseSubdirectories = true,
        };

        return directory
            .EnumerateFiles("GlobalSuppressions.cs", enumerationOptions)
            .Select(Get)
            .Aggregate(RuleViolationStatistics.Empty, (s, rvs) => s.Add(rvs));
    }
}
