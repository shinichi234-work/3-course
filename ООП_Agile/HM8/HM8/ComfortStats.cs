namespace HM8;

public class ComfortStats
{
    private const int DryThreshold = 30;

    private int _dryCount;

    public void OnHumidityChanged(HumiditySensor sender, int percent)
    {
        if (percent < DryThreshold)
        {
            _dryCount++;
        }
    }

    public void Report()
    {
        Console.WriteLine();
        Console.WriteLine("=== Статистика комфорта ===");
        Console.WriteLine(
            $"Слишком сухо (<{DryThreshold}%) было {_dryCount} " +
            GetTimesWord(_dryCount)
        );
    }

    private string GetTimesWord(int count)
    {
        if (count % 10 == 1 && count % 100 != 11)
        {
            return "раз";
        }
        if (count % 10 >= 2 && count % 10 <= 4 && 
            (count % 100 < 10 || count % 100 >= 20))
        {
            return "раза";
        }
        return "раз";
    }
}