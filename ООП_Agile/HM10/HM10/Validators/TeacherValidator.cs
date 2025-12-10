namespace HM10.Validators;

public static class TeacherValidator
{
    public static void Validate(Models.Teacher teacher)
    {
        if (string.IsNullOrWhiteSpace(teacher.Name))
        {
            throw new ArgumentException(
                "Имя преподавателя не может быть пустым"
            );
        }

        if (!string.IsNullOrWhiteSpace(teacher.Email))
        {
            if (!teacher.Email.Contains("@"))
            {
                throw new ArgumentException("Некорректный email");
            }
        }
    }
}