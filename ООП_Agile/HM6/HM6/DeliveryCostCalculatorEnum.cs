namespace HM6;

public class DeliveryCostCalculatorEnum
{
    private const int BadWeatherSurcharge = 2;
    private const int RushHourSurcharge = 1;
    private const int SubscriptionDiscount = 2;
    private const int MinimumCost = 1;

    public int Calculate(DeliveryZone zone, DeliveryContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        int baseCost = GetBaseCost(zone);
        
        if (zone == DeliveryZone.Campus)
        {
            return baseCost;
        }

        int cost = baseCost;

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

    private int GetBaseCost(DeliveryZone zone)
    {
        return zone switch
        {
            DeliveryZone.Near => 2,
            DeliveryZone.Mid => 4,
            DeliveryZone.Far => 7,
            DeliveryZone.Remote => 10,
            DeliveryZone.Campus => 1,
            _ => throw new ArgumentException(
                "Неизвестная зона доставки"
            )
        };
    }
}