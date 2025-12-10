namespace HM4;

public class SecureSmsNotifier : SmsNotifier
{
    private bool encrypted;

    public bool Encrypted
    {
        get => encrypted;
        set => encrypted = value;
    }

    public SecureSmsNotifier(string sender, string provider) 
        : base(sender, provider)
    {
        encrypted = false;
    }

    public void EnableEncryption(bool on)
    {
        Encrypted = on;
    }

    public override string Send(string to, string text)
    {
        if (!Enabled)
        {
            return "Отключено";
        }

        string encryptionMark = Encrypted ? " [Зашифровано]" : "";
        return $"SMS от {Sender} через {Provider} для {to}: " +
               $"{text}{encryptionMark}";
    }
}