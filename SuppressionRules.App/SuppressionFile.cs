namespace SuppressionRules.App;

internal class SuppressionFile
{
    private readonly FileInfo file;

    internal SuppressionFile(FileInfo file)
    {
        if (!file.Exists) throw new ArgumentException($"The file '{file.FullName}' does not exist.");

        this.file = file;
    }

    internal IEnumerable<string> GetLines()
    {
        var lines = new List<string>();
        using var reader = file.OpenText();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (line is not null)
            {
                lines.Add(line);
            }
        }

        return lines;
    }
}
