namespace HM10.Validators;

public static class CourseValidator
{
    private const int MinDuration = 30;
    private const int MaxDuration = 240;

    public static void Validate(Models.Course course)
    {
        if (string.IsNullOrWhiteSpace(course.Title))
        {
            throw new ArgumentException(
                "Название курса не может быть пустым"
            );
        }

        if (course.Duration < MinDuration || course.Duration > MaxDuration)
        {
            throw new ArgumentException(
                $"Длительность должна быть от {MinDuration} " +
                $"до {MaxDuration} минут"
            );
        }
    }
}