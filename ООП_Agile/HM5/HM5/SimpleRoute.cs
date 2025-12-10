namespace HM5;

public class SimpleRoute : IRoute
{
    private readonly string _from;
    private readonly string _to;
    private readonly int _distanceKm;

    public string From => _from;
    public string To => _to;
    public int DistanceKm => _distanceKm;

    public SimpleRoute(string from, string to, int distanceKm)
    {
        if (string.IsNullOrWhiteSpace(from))
        {
            throw new ArgumentException(
                "Начальная точка не может быть пустой"
            );
        }

        if (string.IsNullOrWhiteSpace(to))
        {
            throw new ArgumentException(
                "Конечная точка не может быть пустой"
            );
        }

        if (distanceKm <= 0)
        {
            throw new ArgumentException(
                "Расстояние должно быть положительным"
            );
        }

        _from = from;
        _to = to;
        _distanceKm = distanceKm;
    }
}