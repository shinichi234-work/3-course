namespace HM5;

public class Program
{
    private const int TestSpeedKmh = 90;
    private const int TestDistanceKm = 120;

    public static void Main()
    {
        var simpleRoute = new SimpleRoute(
            "Москва", 
            "Тверь", 
            TestDistanceKm
        );

        var tollRoute = new TollRoute(
            "Москва", 
            "Санкт-Петербург", 
            TestDistanceKm, 
            3, 
            150
        );

        Console.WriteLine(" Простой маршрут ");
        DisplayRouteInfo(simpleRoute, TestSpeedKmh);
        Console.WriteLine();

        Console.WriteLine(" Платный маршрут ");
        DisplayRouteInfo(tollRoute, TestSpeedKmh);
        if (tollRoute is ITollAware tollAware)
        {
            Console.WriteLine(
                $"Количество пунктов оплаты: {tollAware.TollCount}"
            );
            Console.WriteLine(
                $"Общая стоимость проезда: " +
                $"{tollAware.TotalTollCost()} руб."
            );
        }
        Console.WriteLine();

        Console.WriteLine(" Сравнение времени в пути ");
        CompareRoutes(simpleRoute, tollRoute, TestSpeedKmh);
    }

    private static void DisplayRouteInfo(IRoute route, int speedKmh)
    {
        Console.WriteLine($"Маршрут: {route.From} → {route.To}");
        Console.WriteLine($"Расстояние: {route.DistanceKm} км");
        Console.WriteLine(
            $"Время в пути (при {speedKmh} км/ч): " +
            $"{route.EstimateMinutes(speedKmh)} мин"
        );
    }

    private static void CompareRoutes(
        IRoute route1, 
        IRoute route2, 
        int speedKmh
    )
    {
        int time1 = route1.EstimateMinutes(speedKmh);
        int time2 = route2.EstimateMinutes(speedKmh);
        int difference = Math.Abs(time2 - time1);

        Console.WriteLine(
            $"Простой маршрут: {time1} мин"
        );
        Console.WriteLine(
            $"Платный маршрут: {time2} мин"
        );
        Console.WriteLine(
            $"Разница во времени: {difference} мин"
        );
    }
}