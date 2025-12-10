namespace HM4;

public class SmsNotifier : Notifier
{
    private string provider = string.Empty;

    public string Provider
    {
        get => provider;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Провайдер не может быть пустым"
                );
            }
            provider = value;
        }
    }

    public SmsNotifier(string sender, string provider) 
        : base(sender)
    {
        Provider = provider;
    }

    public void SetProvider(string p)
    {
        Provider = p;
    }
}