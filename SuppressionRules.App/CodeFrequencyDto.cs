namespace SuppressionRules.App;

internal class CodeFrequencyDto
{
    public string Code { get; }

    public int Frequency { get; }

    public CodeFrequencyDto(string code, int frequency)
    {
        Code = code;
        Frequency = frequency;
    }
}