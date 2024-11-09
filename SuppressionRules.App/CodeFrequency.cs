namespace SuppressionRules.App;

internal class CodeFrequency
{
    internal string Code { get; }

    internal int Frequency { get; }
    
    internal CodeFrequency(string code, int frequency)
    {
        Code = code;
        Frequency = frequency;
    }
}
