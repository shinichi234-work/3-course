namespace HM4;

public class EmailNotifier : Notifier
{
    private string smtpHost = string.Empty;

    public string SmtpHost
    {
        get => smtpHost;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "SMTP хост не может быть пустым"
                );
            }
            smtpHost = value;
        }
    }

    public EmailNotifier(string sender, string smtpHost) 
        : base(sender)
    {
        SmtpHost = smtpHost;
    }

    public void Configure(string host)
    {
        SmtpHost = host;
    }

    public override string Send(string to, string text)
    {
        if (!Enabled)
        {
            return "Отключено";
        }

        return $"Email от {Sender} через {SmtpHost} для {to}: {text}";
    }
}