namespace HM8;

public class Program
{
    public static void Main()
    {
        var sensor = new HumiditySensor();
        var panel = new ConsolePanel();
        var stats = new ComfortStats();

        sensor.HumidityChanged += panel.OnHumidityChanged;
        sensor.HumidityChanged += stats.OnHumidityChanged;
        sensor.MoldRiskReached += panel.OnMoldRiskReached;

        Console.WriteLine(" Запуск мониторинга влажности ");
        Console.WriteLine();

        sensor.Start();

        Console.WriteLine();
        Console.WriteLine(" Отписка ConsolePanel от MoldRiskReached ");
        sensor.MoldRiskReached -= panel.OnMoldRiskReached;

        stats.Report();
    }
}