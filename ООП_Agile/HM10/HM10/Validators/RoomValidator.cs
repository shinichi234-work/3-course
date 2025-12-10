namespace HM10.Validators;

public static class RoomValidator
{
    private const int MinCapacity = 1;
    private const int MaxCapacity = 500;

    public static void Validate(Models.Room room)
    {
        if (string.IsNullOrWhiteSpace(room.Code))
        {
            throw new ArgumentException("Код аудитории не может быть пустым");
        }

        if (room.Capacity < MinCapacity || room.Capacity > MaxCapacity)
        {
            throw new ArgumentException(
                $"Вместимость должна быть от {MinCapacity} до {MaxCapacity}"
            );
        }
    }
}