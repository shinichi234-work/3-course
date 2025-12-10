namespace HM6;

public class DeliveryCostCalculatorOop
{
    public int Calculate(Zone zone, DeliveryContext context)
    {
        if (zone == null)
        {
            throw new ArgumentNullException(nameof(zone));
        }

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        return zone.GetDeliveryCost(context);
    }
}