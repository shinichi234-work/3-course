namespace HM6;

public abstract class Zone
{
    protected const int BadWeatherSurcharge = 2;
    protected const int RushHourSurcharge = 1;
    protected const int SubscriptionDiscount = 2;
    protected const int MinimumCost = 1;

    private readonly int _baseCost;

    public int BaseCost => _baseCost;

    protected Zone(int baseCost)
    {
        if (baseCost < 0)
        {
            throw new ArgumentException(
                "Базовая стоимость не может быть отрицательной"
            );
        }

        _baseCost = baseCost;
    }

    public virtual int GetDeliveryCost(DeliveryContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        int cost = BaseCost;

        if (context.IsBadWeather)
        {
            cost += BadWeatherSurcharge;
        }

        if (context.IsRushHour)
        {
            cost += RushHourSurcharge;
        }

        if (context.HasSubscription)
        {
            cost -= SubscriptionDiscount;
        }

        return Math.Max(cost, MinimumCost);
    }
}