namespace HM10.Validators;

public static class SessionValidator
{
    public static void Validate(Models.Session session)
    {
        if (session.CourseId <= 0)
        {
            throw new ArgumentException("Некорректный ID курса");
        }

        if (session.TeacherId <= 0)
        {
            throw new ArgumentException("Некорректный ID преподавателя");
        }

        if (session.GroupId <= 0)
        {
            throw new ArgumentException("Некорректный ID группы");
        }

        if (session.RoomId <= 0)
        {
            throw new ArgumentException("Некорректный ID аудитории");
        }

        if (session.StartTime >= session.EndTime)
        {
            throw new ArgumentException(
                "Время начала должно быть раньше времени окончания"
            );
        }

        var duration = session.EndTime - session.StartTime;
        if (duration.TotalMinutes < 30)
        {
            throw new ArgumentException(
                "Минимальная длительность занятия - 30 минут"
            );
        }

        if (duration.TotalMinutes > 240)
        {
            throw new ArgumentException(
                "Максимальная длительность занятия - 240 минут"
            );
        }
    }
}