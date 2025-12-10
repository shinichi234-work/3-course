namespace HM9;

public class PointOfInterest
{
    private const int MinimumImportance = 1;

    public string Code { get; }
    public string Label { get; }
    public int Importance { get; }

    public PointOfInterest(string code, string label, int importance)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException(
                "Код POI не может быть пустым"
            );
        }

        if (string.IsNullOrWhiteSpace(label))
        {
            throw new ArgumentException(
                "Название POI не может быть пустым"
            );
        }

        if (importance < MinimumImportance)
        {
            throw new ArgumentException(
                "Важность должна быть не меньше 1"
            );
        }

        Code = code;
        Label = label;
        Importance = importance;
    }

    public override string ToString()
    {
        return $"{Label} ({Code}, важность: {Importance})";
    }
}