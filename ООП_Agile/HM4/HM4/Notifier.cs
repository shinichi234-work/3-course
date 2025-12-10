namespace HM4;

public class Notifier
{
    private string sender = string.Empty;
    private bool enabled;

    public string Sender
    {
        get => sender;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Отправитель не может быть пустым"
                );
            }
            sender = value;
        }
    }

    public bool Enabled
    {
        get => enabled;
        set => enabled = value;
    }

    public Notifier(string sender)
    {
        Sender = sender;
        enabled = false;
    }

    public void Enable()
    {
        Enabled = true;
    }

    public void Disable()
    {
        Enabled = false;
    }

    public virtual string Send(string to, string text)
    {
        if (!Enabled)
        {
            return "Отключено";
        }

        return $"Базовая отправка для {to}: {text}";
    }
}