namespace HM3;

public class Guitar : Instrument
{
    private const int MinimumStringCount = 1;

    public int StringCount { get; }

    public Guitar(string material, int stringCount) 
        : base("Гитара", material)
    {
        if (stringCount < MinimumStringCount)
        {
            throw new ArgumentException(
                "Количество струн должно быть положительным",
                nameof(stringCount)
            );
        }

        StringCount = stringCount;
    }

    public override string PlayMusic()
    {
        return $"Играет {InstrumentName}: звенит " +
               $"{StringCount}-струнная гитара.";
    }
}