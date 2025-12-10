namespace HM5;

public class TollRoute : IRoute, ITollAware
{
    private const int MinutesDelayPerToll = 2;
    private const int MinimumMinutes = 1;

    private readonly string _from;
    private readonly string _to;
    private readonly int _distanceKm;
    private readonly int _tollCount;
    private readonly int _tollPricePerGate;

    public string From => _from;
    public string To => _to;
    public int DistanceKm => _distanceKm;
    public int TollCount => _tollCount;
    public int TollPricePerGate => _tollPricePerGate;

    public TollRoute(
        string from, 
        string to, 
        int distanceKm, 
        int tollCount, 
        int tollPricePerGate
    )
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

        if (tollCount < 0)
        {
            throw new ArgumentException(
                "Количество пунктов оплаты не может быть отрицательным"
            );
        }

        if (tollPricePerGate < 0)
        {
            throw new ArgumentException(
                "Цена за проезд не может быть отрицательной"
            );
        }

        _from = from;
        _to = to;
        _distanceKm = distanceKm;
        _tollCount = tollCount;
        _tollPricePerGate = tollPricePerGate;
    }

    public int EstimateMinutes(int avgSpeedKmh)
    {
        if (avgSpeedKmh <= 0)
        {
            throw new ArgumentException(
                "Средняя скорость должна быть положительной"
            );
        }

        int baseMinutes = (int)Math.Ceiling(
            DistanceKm * 60.0 / avgSpeedKmh
        );
        int tollDelay = TollCount * MinutesDelayPerToll;
        int totalMinutes = baseMinutes + tollDelay;

        return Math.Max(MinimumMinutes, totalMinutes);
    }

    public int TotalTollCost()
    {
        return TollCount * TollPricePerGate;
    }
}