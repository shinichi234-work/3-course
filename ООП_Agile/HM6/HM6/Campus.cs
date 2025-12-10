namespace HM6;

public class Campus : Zone
{
    private const int CampusBaseCost = 1;

    public Campus() : base(CampusBaseCost)
    {
    }

    public override int GetDeliveryCost(DeliveryContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        return BaseCost;
    }
}