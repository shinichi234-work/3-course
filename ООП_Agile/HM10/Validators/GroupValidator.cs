namespace HM10.Validators;

public static class GroupValidator
{
    private const int MinSize = 1;
    private const int MaxSize = 100;

    public static void Validate(Models.Group group)
    {
        if (string.IsNullOrWhiteSpace(group.Code))
        {
            throw new ArgumentException("Код группы не может быть пустым");
        }

        if (group.Size < MinSize || group.Size > MaxSize)
        {
            throw new ArgumentException(
                $"Размер группы должен быть от {MinSize} до {MaxSize}"
            );
        }

        if (group.Year < 2000 || group.Year > 2100)
        {
            throw new ArgumentException("Некорректный год");
        }
    }
}
