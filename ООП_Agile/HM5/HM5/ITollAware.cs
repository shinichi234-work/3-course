namespace HM5;

public interface ITollAware
{
    int TollCount { get; }
    int TotalTollCost();
}