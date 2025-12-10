namespace HM6;

public class Program
{
    public static void Main()
    {
        TestEnumApproach();
        Console.WriteLine();
        TestOopApproach();
    }

    private static void TestEnumApproach()
    {
        Console.WriteLine(" Тест enum-подхода ");
        var calculator = new DeliveryCostCalculatorEnum();

        var test1 = new DeliveryContext();
        Console.WriteLine(
            $"Near без модификаторов: " +
            $"{calculator.Calculate(DeliveryZone.Near, test1)}"
        );

        var test2 = new DeliveryContext(isRushHour: true);
        Console.WriteLine(
            $"Mid с IsRushHour: " +
            $"{calculator.Calculate(DeliveryZone.Mid, test2)}"
        );

        var test3 = new DeliveryContext(hasSubscription: true);
        Console.WriteLine(
            $"Far с HasSubscription: " +
            $"{calculator.Calculate(DeliveryZone.Far, test3)}"
        );

        var test4 = new DeliveryContext(
            isBadWeather: true, 
            isRushHour: true
        );
        Console.WriteLine(
            $"Remote с IsBadWeather и IsRushHour: " +
            $"{calculator.Calculate(DeliveryZone.Remote, test4)}"
        );

        var test5 = new DeliveryContext(
            isBadWeather: true, 
            isRushHour: true, 
            hasSubscription: true
        );
        Console.WriteLine(
            $"Campus с любым контекстом: " +
            $"{calculator.Calculate(DeliveryZone.Campus, test5)}"
        );
    }

    private static void TestOopApproach()
    {
        Console.WriteLine(" Тест ООП-подхода ");
        var calculator = new DeliveryCostCalculatorOop();

        var test1 = new DeliveryContext();
        Console.WriteLine(
            $"Near без модификаторов: " +
            $"{calculator.Calculate(new Near(), test1)}"
        );

        var test2 = new DeliveryContext(isRushHour: true);
        Console.WriteLine(
            $"Mid с IsRushHour: " +
            $"{calculator.Calculate(new Mid(), test2)}"
        );

        var test3 = new DeliveryContext(hasSubscription: true);
        Console.WriteLine(
            $"Far с HasSubscription: " +
            $"{calculator.Calculate(new Far(), test3)}"
        );

        var test4 = new DeliveryContext(
            isBadWeather: true, 
            isRushHour: true
        );
        Console.WriteLine(
            $"Remote с IsBadWeather и IsRushHour: " +
            $"{calculator.Calculate(new Remote(), test4)}"
        );

        var test5 = new DeliveryContext(
            isBadWeather: true, 
            isRushHour: true, 
            hasSubscription: true
        );
        Console.WriteLine(
            $"Campus с любым контекстом: " +
            $"{calculator.Calculate(new Campus(), test5)}"
        );
    }
}