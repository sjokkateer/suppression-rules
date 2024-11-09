using Cocona;

namespace SuppressionRules.App;

internal class RuleCommands
{
    private readonly RuleViolationStatisticsService service;
    private readonly JsonReader reader;
    private readonly JsonWriter writer;

    public RuleCommands(
        RuleViolationStatisticsService service,
        JsonReader reader,
        JsonWriter writer
    )
    {
        this.service = service;
        this.reader = reader;
        this.writer = writer;
    }

    [Command(nameof(Frequency))]
    public void Frequency([Argument] string pathToFile, [Argument] IEnumerable<string>? rules)
    {
        var file = new FileInfo(pathToFile);
        var stats = service.Get(file);

        var filteredStats = stats.Only(rules);

        var outputObject = RuleViolationStatisticsDto.Create(file.FullName, filteredStats);
        var output = writer.Write(outputObject);

        Console.WriteLine(output);
    }

    [Command(nameof(Frequencies))]
    public void Frequencies([Argument] string pathToDirectory, [Argument] IEnumerable<string>? rules)
    {
        var dir = new DirectoryInfo(pathToDirectory);
        var stats = service.Get(dir);

        var filteredStats = stats.Only(rules);

        var outputObject = RuleViolationStatisticsDto.Create(dir.FullName, filteredStats);
        var output = writer.Write(outputObject);

        Console.WriteLine(output);
    }

    [Command(nameof(Union))]
    public void Union([Argument] string one, [Argument] string other)
    {
        var oneRvsDto = reader.Read(one);
        var otherRvsDto = reader.Read(other);

        if (oneRvsDto is null || otherRvsDto is null)
            throw new InvalidRuleViolationStatisticsDtoException(writer, nameof(Union));

        var oneRvs = RuleViolationStatisticsMapper.Map(oneRvsDto);
        var otherRvs = RuleViolationStatisticsMapper.Map(otherRvsDto);

        var union = oneRvs.Add(otherRvs);

        var outputObject = RuleViolationStatisticsDto.Create($"{oneRvsDto.Source} U {otherRvsDto.Source}", union);
        var output = writer.Write(outputObject);

        Console.WriteLine(output);
    }

    [Command(nameof(Except))]
    public void Except([Argument] string one, [Argument] string other)
    {
        var oneRvsDto = reader.Read(one);
        var otherRvsDto = reader.Read(other);

        if (oneRvsDto is null || otherRvsDto is null)
            throw new InvalidRuleViolationStatisticsDtoException(writer, nameof(Except));

        var oneRvs = RuleViolationStatisticsMapper.Map(oneRvsDto);
        var otherRvs = RuleViolationStatisticsMapper.Map(otherRvsDto);

        var diff = oneRvs.Except(otherRvs);

        var outputObject = RuleViolationStatisticsDto.Create($@"{oneRvsDto.Source} \ {otherRvsDto.Source}", diff);
        var output = writer.Write(outputObject);

        Console.WriteLine(output);
    }
}
