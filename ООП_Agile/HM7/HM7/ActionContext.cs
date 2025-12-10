namespace HM7;

public class ActionContext
{
    public string User { get; set; }
    public string Action { get; set; }
    public DateTime Timestamp { get; set; }

    public ActionContext(string user, string action, DateTime timestamp)
    {
        if (string.IsNullOrWhiteSpace(user))
        {
            throw new ArgumentException(
                "Имя пользователя не может быть пустым"
            );
        }

        if (string.IsNullOrWhiteSpace(action))
        {
            throw new ArgumentException(
                "Действие не может быть пустым"
            );
        }

        User = user;
        Action = action;
        Timestamp = timestamp;
    }

    public override string ToString()
    {
        return $"[{Timestamp:HH:mm:ss}] {User}: {Action}";
    }
}