namespace HM1;

public class Phone
{
    public string Model { get; }
    public string Carrier { get; }
    public Person Owner { get; }

    public Phone(string model, string carrier, Person owner)
    {
        if (string.IsNullOrWhiteSpace(model))
        {
            throw new ArgumentException(
                "Модель не может быть пустой",
                nameof(model)
            );
        }

        if (string.IsNullOrWhiteSpace(carrier))
        {
            throw new ArgumentException(
                "Оператор связи не может быть пустым",
                nameof(carrier)
            );
        }

        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        Model = model;
        Carrier = carrier;
        Owner = owner;
    }

    public override string ToString()
    {
        return $"{Model} ({Carrier}) - Владелец: {Owner.FullName}";
    }
}