namespace HM3;

public class Piano : Instrument
{
    private const int MinimumKeyCount = 1;

    public int KeyCount { get; }

    public Piano(string material, int keyCount) 
        : base("Пианино", material)
    {
        if (keyCount < MinimumKeyCount)
        {
            throw new ArgumentException(
                "Количество клавиш должно быть положительным",
                nameof(keyCount)
            );
        }

        KeyCount = keyCount;
    }

    public override string PlayMusic()
    {
        return $"Играет {InstrumentName}: звучат мелодии на " +
               $"{KeyCount} клавишах.";
    }
}