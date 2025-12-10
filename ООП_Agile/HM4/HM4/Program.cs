namespace HM4;

public class Program
{
    public static void Main()
    {
        var baseNotifier = new Notifier("Система");
        Console.WriteLine(baseNotifier.Send("user1", "Привет"));
        baseNotifier.Enable();
        Console.WriteLine(baseNotifier.Send("user1", "Привет"));
        Console.WriteLine();

        var emailNotifier = new EmailNotifier(
            "admin@example.com", 
            "smtp.example.com"
        );
        emailNotifier.Enable();
        Console.WriteLine(emailNotifier.Send(
            "user@example.com", 
            "Важное письмо"
        ));
        emailNotifier.Configure("smtp.newhost.com");
        Console.WriteLine(emailNotifier.Send(
            "user@example.com", 
            "Обновление"
        ));
        Console.WriteLine();

        var smsNotifier = new SmsNotifier("СМС-Центр", "МТС");
        smsNotifier.Enable();
        Console.WriteLine(smsNotifier.Send("+79001234567", "Код: 1234"));
        smsNotifier.SetProvider("Билайн");
        Console.WriteLine(smsNotifier.Send("+79001234567", "Код: 5678"));
        Console.WriteLine();

        var secureNotifier = new SecureSmsNotifier(
            "Банк", 
            "Мегафон"
        );
        secureNotifier.Enable();
        Console.WriteLine(secureNotifier.Send(
            "+79009876543", 
            "Ваш баланс: 5000"
        ));
        secureNotifier.EnableEncryption(true);
        Console.WriteLine(secureNotifier.Send(
            "+79009876543", 
            "Ваш баланс: 5000"
        ));
    }
}