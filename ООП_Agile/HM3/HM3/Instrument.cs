namespace HM3;

public abstract class Instrument
{
    public string InstrumentName { get; }
    public string Material { get; }

    protected Instrument(string instrumentName, string material)
    {
        if (string.IsNullOrWhiteSpace(instrumentName))
        {
            throw new ArgumentException(
                "Название инструмента не может быть пустым",
                nameof(instrumentName)
            );
        }

        if (string.IsNullOrWhiteSpace(material))
        {
            throw new ArgumentException(
                "Материал не может быть пустым",
                nameof(material)
            );
        }

        InstrumentName = instrumentName;
        Material = material;
    }

    public abstract string PlayMusic();
}