namespace HM6;

public class DeliveryContext
{
    public bool IsBadWeather { get; set; }
    public bool IsRushHour { get; set; }
    public bool HasSubscription { get; set; }

    public DeliveryContext(
        bool isBadWeather = false,
        bool isRushHour = false,
        bool hasSubscription = false
    )
    {
        IsBadWeather = isBadWeather;
        IsRushHour = isRushHour;
        HasSubscription = hasSubscription;
    }
}