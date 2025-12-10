namespace HM8;

public class HumiditySensor
{
    private const int MinHumidity = 20;
    private const int MaxHumidity = 95;
    private const int MinReadings = 8;
    private const int MaxReadings = 12;
    private const int MoldRiskThreshold = 80;

    public delegate void HumidityEventHandler(
        HumiditySensor sender,
        int percent
    );

    public event HumidityEventHandler? HumidityChanged;

    private EventHandler<int>? _moldRiskReached;

    public event EventHandler<int> MoldRiskReached
    {
        add
        {
            _moldRiskReached += value;
            Console.WriteLine(
                "[СИСТЕМА] Подписчик добавлен к MoldRiskReached"
            );
        }
        remove
        {
            _moldRiskReached -= value;
            Console.WriteLine(
                "[СИСТЕМА] Подписчик удалён из MoldRiskReached"
            );
        }
    }

    public void Start()
    {
        var random = new Random();
        int readingsCount = random.Next(MinReadings, MaxReadings + 1);

        Console.WriteLine(
            $"[ДАТЧИК] Начало измерений ({readingsCount} показаний)"
        );
        Console.WriteLine();

        for (int i = 0; i < readingsCount; i++)
        {
            int humidity = random.Next(MinHumidity, MaxHumidity + 1);

            HumidityChanged?.Invoke(this, humidity);

            if (humidity >= MoldRiskThreshold)
            {
                _moldRiskReached?.Invoke(this, humidity);
            }

            Thread.Sleep(300);
        }

        Console.WriteLine();
        Console.WriteLine("[ДАТЧИК] Измерения завершены");
    }
}