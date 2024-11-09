using System.Text.RegularExpressions;

namespace SuppressionRules.App;

internal class RuleRegex
{
    private const string CapturingGroup = "rule";

    private static readonly Regex regex = new Regex($"SuppressMessage\\(\".+?\",\\s*\"(?<{CapturingGroup}>[A-Z]+[0-9]+):.*\"\\)");

    internal static string? Get(string line)
    {
        var match = regex.Match(line);

        if (!match.Success) return null;

        return match.Groups[CapturingGroup].Value;
    }
}
