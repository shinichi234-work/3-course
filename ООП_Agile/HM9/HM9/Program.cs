namespace HM9;

public class Program
{
    public static void Main()
    {
        var atlas = new Atlas();

        var village = new Location("loc_001", "Деревня Светлая", 
            RegionRank.Local);
        village.AddPoi(new PointOfInterest("inn", "Таверна", 3));
        village.AddPoi(new PointOfInterest("well", "Колодец", 1));

        var city = new Location("loc_002", "Город Каменный", 
            RegionRank.Regional);
        city.AddPoi(new PointOfInterest("market", "Рынок", 5));
        city.AddPoi(new PointOfInterest("guild", "Гильдия", 4));
        city.AddPoi(new PointOfInterest("temple", "Храм", 6));

        var capital = new Location("loc_003", "Столица Золотая", 
            RegionRank.Capital);
        capital.AddPoi(new PointOfInterest("palace", "Дворец", 10));
        capital.AddPoi(new PointOfInterest("arena", "Арена", 8));

        var ruins = new Location("loc_004", "Древние руины", 
            RegionRank.Sacred);
        ruins.AddPoi(new PointOfInterest("altar", "Алтарь", 9));

        atlas.Add(village);
        atlas.Add(city);
        atlas.Add(capital);
        atlas.Add(ruins);

        Console.WriteLine(" Все локации в атласе ");
        foreach (var loc in atlas)
        {
            Console.WriteLine(loc);
        }
        Console.WriteLine();

        Console.WriteLine(" Доступ по индексу [1] ");
        Console.WriteLine(atlas[1]);
        Console.WriteLine();

        Console.WriteLine(" Доступ по Id [loc_003] ");
        var foundLoc = atlas["loc_003"];
        Console.WriteLine(foundLoc);
        Console.WriteLine("Точки интереса:");
        foreach (var poi in foundLoc.PointsOfInterest)
        {
            Console.WriteLine($"  - {poi}");
        }
        Console.WriteLine();

        Console.WriteLine(" Локации с рангом >= Regional ");
        foreach (var loc in atlas.EnumerateByRank(RegionRank.Regional))
        {
            Console.WriteLine(loc);
        }
        Console.WriteLine();

        Console.WriteLine(" Удаление локации по индексу 0 ");
        bool removed = atlas.RemoveAt(0);
        Console.WriteLine($"Удалено: {removed}");
        Console.WriteLine($"Количество локаций: {atlas.Count}");
        Console.WriteLine();

        Console.WriteLine(" Удаление локации по Id loc_004 ");
        removed = atlas.RemoveById("loc_004");
        Console.WriteLine($"Удалено: {removed}");
        Console.WriteLine($"Количество локаций: {atlas.Count}");
        Console.WriteLine();

        Console.WriteLine(" Оставшиеся локации ");
        foreach (var loc in atlas)
        {
            Console.WriteLine(loc);
        }
    }
}