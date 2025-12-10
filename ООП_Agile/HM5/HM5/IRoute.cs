namespace HM5;

public interface IRoute
{
    string From { get; }
    string To { get; }
    int DistanceKm { get; }

    int EstimateMinutes(int avgSpeedKmh)
    {
        if (avgSpeedKmh <= 0)
        {
            throw new ArgumentException(
                "Средняя скорость должна быть положительной"
            );
        }

        int minutes = (int)Math.Ceiling(
            DistanceKm * 60.0 / avgSpeedKmh
        );
        return Math.Max(1, minutes);
    }
}