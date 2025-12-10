namespace HM7;

public class Program
{
    public static void Main()
    {
        var logger = new ActionLogger();

        LogHandler uppercaseUser = ctx => 
        {
            ctx.User = ctx.User.ToUpper();
        };

        logger.AddHandler(LogHandlers.LogToConsole);
        logger.AddHandler(LogHandlers.AddTimestampSuffix);
        logger.AddHandler(uppercaseUser);

        Console.WriteLine(" Первый запуск (все обработчики) ");
        var context1 = new ActionContext(
            "Иван Петров",
            "Вход в систему",
            DateTime.Now
        );
        logger.Run(context1);
        Console.WriteLine();

        logger.RemoveHandler(LogHandlers.AddTimestampSuffix);

        Console.WriteLine(" Второй запуск (без AddTimestampSuffix) ");
        var context2 = new ActionContext(
            "Анна Смирнова",
            "Клик на кнопку",
            DateTime.Now
        );
        logger.Run(context2);
        Console.WriteLine();

        Console.WriteLine(" Третий запуск (добавление нового действия) ");
        var context3 = new ActionContext(
            "Петр Сидоров",
            "Выход из системы",
            DateTime.Now
        );
        logger.Run(context3);
        Console.WriteLine();

        LogHandler countActions = ctx =>
        {
            Console.WriteLine($"[СЧЕТЧИК] Обработано действие от {ctx.User}");
        };

        logger.AddHandler(countActions);

        Console.WriteLine(" Четвертый запуск (с новым обработчиком) ");
        var context4 = new ActionContext(
            "Мария Иванова",
            "Обновление профиля",
            DateTime.Now
        );
        logger.Run(context4);
    }
}