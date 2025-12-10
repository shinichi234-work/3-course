namespace HM8;

public class ConsolePanel
{
    private const int MoldRiskThreshold = 80;

    public void OnHumidityChanged(HumiditySensor sender, int percent)
    {
        Console.WriteLine($"Влажность: {percent}%");
    }

    public void OnMoldRiskReached(object? sender, int percent)
    {
        Console.WriteLine(
            $"⚠ Высокая влажность: {percent}% — проветрите помещение"
        );
    }
}