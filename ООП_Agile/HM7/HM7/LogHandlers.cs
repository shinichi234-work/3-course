namespace HM7;

public class LogHandlers
{
    public static void LogToConsole(ActionContext context)
    {
        Console.WriteLine($"[КОНСОЛЬ] {context}");
    }

    public static void AddTimestampSuffix(ActionContext context)
    {
        string timeFormat = context.Timestamp.ToString("HH:mm:ss");
        context.Action = $"{context.Action} (время: {timeFormat})";
    }
}